using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;

namespace MyApp.Api.Exceptions
{
    public static class ConfigureProblemDetails
    {
        public static void AddProblemDetails(this IServiceCollection services, IWebHostEnvironment environment)
        {
            services.AddProblemDetails(options =>
            {
                options.Map<ValidationException>(ex => new ValidationProblemDetails(ex));
                options.IncludeExceptionDetails = (ctx, ex) => environment.IsDevelopment();
                // This will map NotImplementedException to the 501 Not Implemented status code.
                options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);

                // This will map HttpRequestException to the 503 Service Unavailable status code.
                options.MapToStatusCode<HttpRequestException>(StatusCodes.Status503ServiceUnavailable);
                // Because exceptions are handled polymorphically, this will act as a "catch all" mapping, which is why it's added last.
                // If an exception other than NotImplementedException and HttpRequestException is thrown, this will handle it.
                options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
            });
        }
    }
}
