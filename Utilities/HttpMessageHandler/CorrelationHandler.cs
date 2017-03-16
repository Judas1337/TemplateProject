using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApiTemplateProject.Utilities.Concurrency;

namespace WebApiTemplateProject.Utilities.HttpMessageHandler
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
            var correlationIds = GetOrCreateCorrelationIds(request);
            request.Headers.Add(CorrelationIdHeaderName, correlationIds);
            _executionContextValueProvider.SetCorrelationId(string.Join(",", correlationIds));

            var response = await base.SendAsync(request, cancellationToken);
            response.Headers.Add(CorrelationIdHeaderName, correlationIds);
            return response;
        }

        private IEnumerable<string> GetOrCreateCorrelationIds(HttpRequestMessage request)
        {
            bool correlationHeaderValueExists = request.Headers.TryGetValues(CorrelationIdHeaderName, out IEnumerable<string> correlationIds);
            if (correlationHeaderValueExists == false) correlationIds = new List<string>() { Guid.NewGuid().ToString() };
            return correlationIds;
        }
    }
}
