
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;


namespace Temp.Utilities
{
    public class MyExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            HttpStatusCode statusCode;
            if(context.Exception is ArgumentException) statusCode = HttpStatusCode.BadRequest;
            else if (context.Exception is HttpResponseException) statusCode = (context.Exception as HttpResponseException).Response.StatusCode;
            else statusCode = HttpStatusCode.InternalServerError;


            context.Result = new TextPlainErrorResult
            {
                Request = context.Request,
                Content = context.Exception.Message,
                StatusCode = statusCode
            };

        }

        private class TextPlainErrorResult : IHttpActionResult
        {
            public HttpRequestMessage Request { get; set; }

            public string Content { get; set; }

            public HttpStatusCode StatusCode { get; set; }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                HttpResponseMessage response = new HttpResponseMessage(StatusCode);
                response.Content = new StringContent(Content);
                response.RequestMessage = Request;
                return Task.FromResult(response);
            }
        }
    }
}