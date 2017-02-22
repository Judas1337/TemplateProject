using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace WebApiTemplateProject.Utilities.ExceptionHandler
{
    public class GlobalExceptionHandler : System.Web.Http.ExceptionHandling.ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            HttpStatusCode statusCode;
            if(context.Exception is ArgumentException) statusCode = HttpStatusCode.BadRequest;
            else if (context.Exception is HttpResponseException) statusCode = ((HttpResponseException) context.Exception).Response.StatusCode;
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