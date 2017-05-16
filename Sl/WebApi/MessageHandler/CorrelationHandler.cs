using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TemplateProject.Utilities.Concurrency;

namespace TemplateProject.Sl.WebApi.MessageHandler
{
    public class CorrelationHandler : DelegatingHandler
    {
        private const string CorrelationIdHeaderName = "X-Correlation-ID";
        private readonly ICorrelationIdProvider<Guid?> _correlationIdProvider;

        public CorrelationHandler(ICorrelationIdProvider<Guid?> correlationIdProvider)
        {
            _correlationIdProvider = correlationIdProvider;
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
            _correlationIdProvider.SetCorrelationId(correlationId);

            var response = await base.SendAsync(request, cancellationToken);
            response.Headers.Add(CorrelationIdHeaderName, correlationIds.First());

            return response;
        }
    }
}
