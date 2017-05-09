using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Microsoft.Rest;
using WebApiTemplateProject.Utilities.Concurrency;
using WebApiTemplateProject.Utilities.Mapper;

namespace WebApiTemplateProject.Utilities.ExceptionHandler
{
    /// <summary>
    /// When registrered as an ExceptionHandler in Global.asax using Httpconfiguration.Services.Replace(typeof(IExceptionHandler), *ExceptionHandler*) 
    /// it will trigger on all unhandled exceptions that are not a HttpResponseException.
    /// </summary>
    public class GlobalExceptionHandler : System.Web.Http.ExceptionHandling.ExceptionHandler
    {
        private readonly IExecutionContextValueProvider _executionContextValueProvider;

        public GlobalExceptionHandler(IExecutionContextValueProvider executionContextValueProvider)
        {
            _executionContextValueProvider = executionContextValueProvider;
        }

        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            var responseMessage = ExceptionToHttpResponse.ToHttpResponseMessage(context.Exception);

            context.Result = new PlainTextErrorResult
            {
                CorrelationIds = _executionContextValueProvider.GetCorrelationId(),
                Request = context.Request,
                ResponseMessage = responseMessage
            };

            return base.HandleAsync(context, cancellationToken);
        }

        private class PlainTextErrorResult : IHttpActionResult
        {
            public string CorrelationIds { private get; set; }
            public HttpRequestMessage Request { private get; set; }
            public HttpResponseMessage ResponseMessage { private get; set; }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                ResponseMessage.Headers.Add("X-Correlation-ID", new List<string> { CorrelationIds });
                ResponseMessage.RequestMessage = Request;

                return Task.FromResult(ResponseMessage);
            }
        }
    }
}