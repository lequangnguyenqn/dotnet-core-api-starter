using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Linq;
using System.Net.Http;

namespace MyApp.Api.Exceptions
{
    public static class ConfigureProblemDetails
    {
        public static void AddProblemDetails(this IServiceCollection services,
            IWebHostEnvironment environment,
            ILogger logger)
        {
            services.AddProblemDetails(options =>
            {
                options.OnBeforeWriteDetails = (ctx, problem) =>
                {
                    problem.Extensions["requestId"] = ctx.TraceIdentifier;
                    problem.Extensions["correlationIid"] = GetCorrelationId(ctx);
                };

                options.Map<ValidationException>(ex => new ValidationProblemDetails(ex));
                options.IncludeExceptionDetails = (ctx, ex) =>
                {
                    logger.Error(ex, "Exception Details:");
                    return false;
                };
                // This will map NotImplementedException to the 501 Not Implemented status code.
                options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);

                // This will map HttpRequestException to the 503 Service Unavailable status code.
                options.MapToStatusCode<HttpRequestException>(StatusCodes.Status503ServiceUnavailable);
                // Because exceptions are handled polymorphically, this will act as a "catch all" mapping, which is why it's added last.
                // If an exception other than NotImplementedException and HttpRequestException is thrown, this will handle it.
                options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
            });
        }

        private static string GetCorrelationId(HttpContext httpContext)
        {
            const string headerKey = "x-correlation-id";
            var header = string.Empty;

            if (httpContext.Request.Headers.TryGetValue(headerKey, out var values))
            {
                header = values.FirstOrDefault();
            }
            else if (httpContext.Response.Headers.TryGetValue(headerKey, out values))
            {
                header = values.FirstOrDefault();
            }

            var correlationId = string.IsNullOrEmpty(header)
                                    ? Guid.NewGuid().ToString()
                                    : header;

            if (!httpContext.Response.Headers.ContainsKey(headerKey))
            {
                httpContext.Response.Headers.Add(headerKey, correlationId);
            }

            return correlationId;
        }
    }
}
