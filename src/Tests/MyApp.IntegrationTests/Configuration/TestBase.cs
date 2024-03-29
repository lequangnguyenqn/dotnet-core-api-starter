﻿using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyApp.Application.Configuration.Data;
using MyApp.Infrastructure.Domain;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyApp.IntegrationTests.Configuration
{
    public class TestBase
    {
        private static IConfiguration _configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
        protected async Task<HttpClient> CreateClient()
        {
            //Dapper using Sqlite unable to cast object of type 'System.string' to type 'System.Guid'
            //So we need to re-write the mapping
            //https://github.com/StackExchange/Dapper/issues/718
            SqlMapper.AddTypeHandler(new GuidTypeHandler());

            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    // Add TestServer
                    webHost.UseTestServer()
                    .UseConfiguration(_configuration)
                    .UseStartup<Api.Startup>();
                })
                .ConfigureServices(services => ConfigureServices(services));

            // Create and start up the host
            var host = await hostBuilder.StartAsync();

            // Create an HttpClient which is setup for the test host
            var client = host.GetTestClient();

            return client;
        }

        private void ConfigureServices(IServiceCollection services)
        {
            string inMemoryConnectionString = _configuration.GetConnectionString("DefaultConnection");
            //EFCore replace SQLServer by Sqlite 
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<MyAppContext>));
            services.Remove(descriptor);
            services.AddDbContext<MyAppContext>(options =>
            {
                options.UseSqlite(inMemoryConnectionString);
            });

            //Dapper replace SQLServer by Sqlite 
            var sqlConnectionFactory = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(ISqlConnectionFactory));
            services.Remove(sqlConnectionFactory);
            services.AddScoped<ISqlConnectionFactory>(p => new SqliteConnectionFactory(inMemoryConnectionString));

            var sp = services.BuildServiceProvider();
            var context = sp.GetRequiredService<MyAppContext>();
            //Re-create DB for every test
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
