using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyApp.Infrastructure.Domain;
using Serilog;

namespace MyApp.Api
{
    public static class Program
    {
        private static IConfiguration configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile(AppsettingsFileNameWithEnvironment(), optional: true)
            .AddEnvironmentVariables()
            .Build();

        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.WithCorrelationIdHeader()
                .Enrich.FromLogContext()
                .CreateLogger();

            try
            {
                Log.Information("Getting the service running...");

                var host = CreateHostBuilder(args).Build();

                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    try
                    {
                        var context = services.GetRequiredService<MyAppContext>();
                        Log.Information("EF appling migration...");
                        context.Database.Migrate();

                        Log.Information("Creating dummy data...");
                        DbInitializer.Initialize(context);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "An error occurred while seeding the database.");
                        throw;
                    }
                }

                host.Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseSerilog()
                    .UseConfiguration(configuration);
                });
        private static string AppsettingsFileNameWithEnvironment()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (string.IsNullOrEmpty(environmentName))
            {
                environmentName = "Production";
            }
            else
            {
                //The deployment tool is set ASPNETCORE_ENVIRONMENT value as lower case
                //So we need to convert first letter to upper case
                environmentName = environmentName.First().ToString().ToUpper() + environmentName.Substring(1);
            }
            return $"appsettings.{environmentName}.json";
        }
    }
}
