using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace WebApiTemplateProject.Api.Utilities
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            HttpStatusCode statusCode;
            if(context.Exception is ArgumentException) statusCode = HttpStatusCode.BadRequest;
            else if (context.Exception is HttpResponseException) statusCode = (context.Exception as HttpResponseException).Response.StatusCode;
            else statusCode = HttpStatusCode.InternalServerError;


            context.Result = new PlainTextErrorResult
            {
                Request = context.Request,
                ResponseMessage = context.Exception?.Message,
                StatusCode = statusCode
            };

        }

        private class PlainTextErrorResult : IHttpActionResult
        {
            public HttpRequestMessage Request { get; set; }

            public string ResponseMessage { get; set; }

            public HttpStatusCode StatusCode { get; set; }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var response = new HttpResponseMessage(StatusCode);
                response.Content = new StringContent(ResponseMessage);
                response.RequestMessage = Request;
                return Task.FromResult(response);
            }
        }
    }
}