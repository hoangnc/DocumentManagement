using DT.Core.Exceptions;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Filters;

namespace DT.Core.Web.Common.Api.WebApi.Filters
{
    internal class GenericErrorResponse
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DtExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {         
            var exception = actionExecutedContext.Exception;
            if (exception != null)
            {
                // we don't handle the http response exception just throw that
                if (exception is HttpResponseException)
                    return;

                if (exception is NotImplementedException)
                    actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
                else if (exception is ArgumentException)
                {
                    var message = exception.Message.Split(new[] { "\r\n" }, StringSplitOptions.None);
                    ExceptionHandling(HttpStatusCode.BadRequest, message[0]);
                }
                else if (exception is ValidationException)
                    ExceptionHandling(HttpStatusCode.BadRequest, Newtonsoft.Json.JsonConvert.SerializeObject(((ValidationException)exception).Failures));
                else if (exception is NotFoundException)
                    ExceptionHandling(HttpStatusCode.NotFound, exception.Message);
                else if (exception is ExistsException)
                    ExceptionHandling(HttpStatusCode.Found, exception.Message);
                else if (exception is ApplicationException)
                    ExceptionHandling(HttpStatusCode.InternalServerError, exception.Message);
                else
                {
                    ExceptionHandling(HttpStatusCode.InternalServerError, exception.Message);
                }
            }
            base.OnException(actionExecutedContext);
        }

        public void ExceptionHandling(HttpStatusCode statusCode, string message)
        {
            
            if (statusCode == HttpStatusCode.InternalServerError)
            {
                if (!message.Contains("401"))
                {
                    message = "Internal Server Error. Unable to call downstream system.";
                }             
            }

            var errorResponse = new GenericErrorResponse
            {
                Message = message,
                StatusCode = statusCode

            };
            var resp = new HttpResponseMessage(statusCode)
            {
                Content = new ObjectContent<GenericErrorResponse>(errorResponse, new JsonMediaTypeFormatter(), "application/json")
            };

            throw new HttpResponseException(resp);
        }
    }
}
