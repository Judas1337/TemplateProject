using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using TemplateProject.Utilities.Concurrency;
using TemplateProject.Utilities.Exceptions;

namespace TemplateProject.Sl.WebApi.ExceptionHandler
{
    /// <summary>
    /// When registrered as an ExceptionHandler in Global.asax using Httpconfiguration.Services.Replace(typeof(IExceptionHandler), *ExceptionHandler*) 
    /// it will trigger on all unhandled exceptions that are not a HttpResponseException.
    /// </summary>
    public class GlobalExceptionHandler : System.Web.Http.ExceptionHandling.ExceptionHandler
    {
        private readonly ICorrelationIdProvider<Guid?> _correlationIdProvider;

        public GlobalExceptionHandler(ICorrelationIdProvider<Guid?> correlationIdProvider)
        {
            _correlationIdProvider = correlationIdProvider;
        }

        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            var statusCode = MapExceptionToHttpStatusCode(context.Exception);

            context.Result = new PlainTextErrorResult
            {
                CorrelationIds = _correlationIdProvider.GetCorrelationId()?.ToString(),
                Request = context.Request,
                ResponseMessage = context.Exception.Message,
                ResponseStatusCode = statusCode
            };

            return base.HandleAsync(context, cancellationToken);
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
            public string CorrelationIds { private get; set; }
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

                response.Headers.Add("X-Correlation-ID", new List<string> { CorrelationIds });
                response.RequestMessage = Request;

                return Task.FromResult(response);
            }
        }
        #endregion
    }
}