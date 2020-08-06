using Newtonsoft.Json;
using System;
using System.Net;

namespace MyApp.Domain.Exceptions
{
    [Serializable]
    public class ExceptionBase : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public ApplicationErrorCodes ErrorCode { get; set; }
        public ExceptionBase(string message, ApplicationErrorCodes errorCode, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(new { StatusCode, ErrorCode, Message }, new Newtonsoft.Json.Converters.StringEnumConverter());
        }
    }
}
