using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Api.Exceptions
{
    public class ValidationProblemDetails: ProblemDetails
    {
        public ValidationProblemDetails(ValidationException exception)
        {
            Title = "Validation failed";
            Status = StatusCodes.Status400BadRequest;
            Detail = exception.Message;
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
        }
    }
}
