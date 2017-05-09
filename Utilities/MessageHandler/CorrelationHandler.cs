using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using WebApiTemplateProject.Utilities.Concurrency;

namespace WebApiTemplateProject.Utilities.MessageHandler
{
    public class CorrelationHandler : DelegatingHandler
    {
        private const string CorrelationIdHeaderName = "X-Correlation-ID";
        private readonly IExecutionContextValueProvider _executionContextValueProvider;

        public CorrelationHandler(IExecutionContextValueProvider executionContextValueProvider)
        {
            _executionContextValueProvider = executionContextValueProvider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            IEnumerable<string> correlationIds;
            bool correlationHeaderValueExists = request.Headers.TryGetValues(CorrelationIdHeaderName, out correlationIds);
            if (correlationHeaderValueExists == false)
            {
               correlationIds = new List<string>() { Guid.NewGuid().ToString() };
               request.Headers.Add(CorrelationIdHeaderName, correlationIds);
            }
          
            _executionContextValueProvider.SetCorrelationId(string.Join(",", correlationIds));

            var response = await base.SendAsync(request, cancellationToken);
            response.Headers.Add(CorrelationIdHeaderName, correlationIds);
            return response;
        }
    }
}
