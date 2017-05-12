using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TemplateProject.Utilities.Concurrency;

namespace TemplateProject.Sl.WebApi.MessageHandler
{
    public class MessageLoggingHandler : DelegatingHandler
    {
        private readonly ICorrelationIdValueProvider<Guid?> _correlationIdValueProvider;

        public MessageLoggingHandler(ICorrelationIdValueProvider<Guid?> correlationIdValueProvider)
        {
            _correlationIdValueProvider = correlationIdValueProvider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var requestContent = await HttpContentToString(request.Content);
            Debug.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [CorrelationId: {_correlationIdValueProvider.GetCorrelationId()}] REQUEST {request.Method} {request.RequestUri.OriginalString} {requestContent}");

            var response = await base.SendAsync(request, cancellationToken);

            var responseContent = await HttpContentToString(response.Content);
            Debug.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [CorrelationId: {_correlationIdValueProvider.GetCorrelationId()}] RESPONSE {response.StatusCode} {responseContent}");

            return response;
        }

        private async Task<string> HttpContentToString(HttpContent content)
        {
            if (content == null) return null;
            await content.LoadIntoBufferAsync();
            var text = await content.ReadAsStringAsync();
            text = text.Replace(Environment.NewLine, "");
            text = text.Replace("\n", "");
            return text;
        }
    }
}
