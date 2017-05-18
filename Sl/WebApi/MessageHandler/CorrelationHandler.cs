using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TemplateProject.Utilities.Concurrency;

namespace TemplateProject.Sl.WebApi.MessageHandler
{
    /// <summary>
    /// This implementation tries to get a correlation id from the header X-Correlation-ID. 
    /// If successful, this value is added to an executioncontext for later consumption and on the corresponding response header.
    /// Else, a correlation id is created and added to the response, request and executioncontext. 
    /// </summary>
    /// <remarks>Even though the header X-Correlation-ID supports multiple correlation id values, this implementation does not.</remarks>
    public class CorrelationHandler : DelegatingHandler
    {
        private const string CorrelationIdHeaderName = "X-Correlation-ID";
        private readonly ICorrelationIdProvider _correlationIdProvider;

        public CorrelationHandler(ICorrelationIdProvider correlationIdProvider)
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

            var correlationId = correlationIds.First();
            _correlationIdProvider.SetCorrelationId(correlationId);

            var response = await base.SendAsync(request, cancellationToken);
            response.Headers.Add(CorrelationIdHeaderName, correlationId);

            return response;
        }
    }
}
