using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MyApp.Domain.Exceptions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Api
{
    public class CustomExceptionHandler
    {
        private readonly ILogger _logger;

        public CustomExceptionHandler(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger<CustomExceptionHandler>();
        }
        public async Task Invoke(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;
            if (ex == null) return;

            ResponseMessage<object> httpErrorDetail;

            if (ex is ExceptionBase)
            {
                var error = (ex as ExceptionBase);
                context.Response.StatusCode = (int)error.StatusCode;
                httpErrorDetail = ResponseMessage<object>.WithFailed(error.Message, "Something went wrong", error.StatusCode);

            }
            if (ex is UnauthorizedAccessException)
            {
                var error = (ex as UnauthorizedAccessException);
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                httpErrorDetail = ResponseMessage<object>.WithFailed(error.Message, "Unauthorized Access", HttpStatusCode.Unauthorized);

            }
            else
            {
                httpErrorDetail = ResponseMessage<object>.WithFailed(ex.Message, "Something went wrong", (HttpStatusCode)context.Response.StatusCode);
            }

            context.Response.ContentType = "application/json";
            _logger.LogError(ex, "An error occurred: {Application}");
            await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(httpErrorDetail)));
        }
    }

    public class ResponseMessage<T>
    {
        public ResponseMessage()
        {
        }

        public static ResponseMessage<T> WithSuccess(T result, string message = "success", HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            ResponseMessage<T> response = new ResponseMessage<T>();
            response.IsSuccess = true;
            response.Result = result;
            response.Message = message;
            response.StatusCode = httpStatusCode;
            return response;
        }

        public static ResponseMessage<T> WithFailed(object errors, string message = "failed", HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            ResponseMessage<T> response = new ResponseMessage<T>();
            response.IsSuccess = false;
            response.Errors = errors;
            response.Message = message;
            response.StatusCode = httpStatusCode;
            return response;
        }

        public bool IsSuccess { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
        public object Errors { get; set; }
    }
}
