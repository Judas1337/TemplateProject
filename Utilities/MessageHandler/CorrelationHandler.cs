using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebApiTemplateProject.Utilities.Concurrency;

namespace WebApiTemplateProject.Utilities.MessageHandler
{
    public class CorrelationHandler : DelegatingHandler
    {
        private const string CorrelationIdHeaderName = "X-Correlation-ID";
        private readonly ICorrelationIdValueProvider<Guid?> _correlationIdValueProvider;

        public CorrelationHandler(ICorrelationIdValueProvider<Guid?> correlationIdValueProvider)
        {
            _correlationIdValueProvider = correlationIdValueProvider;
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

            var correlationId = new Guid(correlationIds.First());
            _correlationIdValueProvider.SetCorrelationId(correlationId);

            var response = await base.SendAsync(request, cancellationToken);
            response.Headers.Add(CorrelationIdHeaderName, correlationIds.First());

            return response;
        }
    }
}
