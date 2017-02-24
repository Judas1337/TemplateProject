using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebApiTemplateProject.Utilities.HttpMessageHandler
{
    public class CorrelationHandler : DelegatingHandler
    {
        private const string CorrelationIdHeaderName = "X-Correlation-ID";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var requestCorrelationId = request.GetCorrelationId().ToString();
            request.Headers.Add(CorrelationIdHeaderName, requestCorrelationId);
            var response = await base.SendAsync(request, cancellationToken);
            response.Headers.Add(CorrelationIdHeaderName, requestCorrelationId);
            return response;
        }
    }
}
