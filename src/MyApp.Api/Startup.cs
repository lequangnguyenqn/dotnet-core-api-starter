using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyApp.Api.Exceptions;
using MyApp.Application.Configuration.Validation;
using MyApp.Infrastructure.Configuration;
using MyApp.Infrastructure.Domain;
using Serilog;

namespace MyApp.Api
{
    public class Startup
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MyAppContext>(options =>
            options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();

            services.AddSwaggerGen();

            services.AddProblemDetails(_environment, Log.Logger);

            services.AddHttpContextAccessor();

            services.AddDataAccessModule(_configuration);

            services.AddMediatR(Assemblies.Application);

            //Add FluentValidation
            services.Add(new ServiceDescriptor(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>), ServiceLifetime.Transient));
            services.AddValidatorsFromAssemblies(new[] { Assemblies.Application });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseProblemDetails();
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Maintenance API V1");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
