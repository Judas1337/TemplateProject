using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Microsoft.Rest;

namespace WebApiTemplateProject.Utilities.ExceptionHandler
{
    /// <summary>
    /// When registrered as an ExceptionHandler in Global.asax using Httpconfiguration.Services.Replace(typeof(IExceptionHandler), *ExceptionHandler*) 
    /// it will trigger on all unhandled exceptions that are not a HttpResponseException.
    /// </summary>
    public class GlobalExceptionHandler : System.Web.Http.ExceptionHandling.ExceptionHandler
    {
        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            HttpStatusCode statusCode;
            if(context.Exception is ArgumentException) statusCode = HttpStatusCode.BadRequest;          
            else if (context.Exception is NotImplementedException) statusCode = HttpStatusCode.NotImplemented;
            else if (context.Exception is HttpOperationException) statusCode = ((HttpOperationException) context.Exception).Response.StatusCode;
            else statusCode = HttpStatusCode.InternalServerError;

            
            context.Result = new PlainTextErrorResult
            {
                Request = context.Request,
                ResponseMessage = context.Exception?.Message,
                StatusCode = statusCode
            };

            return base.HandleAsync(context, cancellationToken);
        }

        private class PlainTextErrorResult : IHttpActionResult
        {
            public HttpRequestMessage Request { private get; set; }

            public string ResponseMessage { private get; set; }

            public HttpStatusCode StatusCode { private get; set; }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var response = new HttpResponseMessage(StatusCode)
                {
                    Content = new StringContent(ResponseMessage),
                    RequestMessage = Request
                };
                return Task.FromResult(response);
            }
        }
    }
}