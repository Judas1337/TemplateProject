using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using TemplateProject.Bll.Contract.Bll.Model.Exceptions;

namespace TemplateProject.Sl.WebApi.ExceptionHandler
{
    /// <summary>
    /// When registrered as an ExceptionHandler in Global.asax using Httpconfiguration.Services.Replace(typeof(IExceptionHandler), *ExceptionHandler*) 
    /// it will trigger on all unhandled exceptions that are not a HttpResponseException.
    /// </summary>
    public class GlobalExceptionHandler : System.Web.Http.ExceptionHandling.ExceptionHandler
    {
        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            var statusCode = MapExceptionToHttpStatusCode(context.Exception);

            context.Result = new PlainTextErrorResult
            {
                CorrelationId = ExtractXCorrelationIdFromRequest(context.Request),
                Request = context.Request,
                ResponseMessage = context.Exception.Message,
                ResponseStatusCode = statusCode
            };

            return base.HandleAsync(context, cancellationToken);
        }

        private static string ExtractXCorrelationIdFromRequest(HttpRequestMessage request)
        {
            IEnumerable<string> correlationIds;
            request.Headers.TryGetValues("X-Correlation-Id", out correlationIds);
            return correlationIds.FirstOrDefault();
        }

        #region Private Helpers
        private static HttpStatusCode MapExceptionToHttpStatusCode(Exception exception)
        {
            HttpStatusCode statusCode;
            if (exception is ConflictException) statusCode = HttpStatusCode.Conflict;
            else if (exception is InputException) statusCode = HttpStatusCode.BadRequest;
            else if (exception is NotFoundException) statusCode = HttpStatusCode.NotFound;
            else if (exception is UnauthorizedException) statusCode = HttpStatusCode.Unauthorized;
            else statusCode = HttpStatusCode.InternalServerError;

            return statusCode;
        }

        private class PlainTextErrorResult : IHttpActionResult
        {
            public string CorrelationId { private get; set; }
            public HttpRequestMessage Request { private get; set; }
            public string ResponseMessage { private get; set; }
            public HttpStatusCode ResponseStatusCode { private get; set; }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var response = new HttpResponseMessage(ResponseStatusCode)
                {
                    Content = new StringContent(ResponseMessage),
                    RequestMessage = Request
                };

                response.Headers.Add("X-Correlation-ID", new List<string> { CorrelationId });
                response.RequestMessage = Request;

                var timeAtResponse = DateTimeOffset.Now;
                var timeTaken = GetTimeTakeForRequestInMilliseconds(Request.Headers.Date, timeAtResponse);
                
                Debug.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [CorrelationId: {CorrelationId}] RESPONSE {response.StatusCode} {timeTaken}ms {ResponseMessage}");
                return Task.FromResult(response);
            }
        }

        private static string GetTimeTakeForRequestInMilliseconds(DateTimeOffset? requestAt, DateTimeOffset responseAt)
        {
            if (requestAt == null) return null;
            var diff = responseAt - requestAt;
            var result = (int) diff.Value.TotalMilliseconds;
            return $"{result}";
        }
        #endregion
    }
}