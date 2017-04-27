using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApiTemplateProject.Utilities.Concurrency;

namespace WebApiTemplateProject.Utilities.MessageHandler
{
    public class MessageLoggingHandler : DelegatingHandler
    {
        private readonly IExecutionContextValueProvider _executionContextValueProvider;

        public MessageLoggingHandler(IExecutionContextValueProvider executionContextValueProvider)
        {
            _executionContextValueProvider = executionContextValueProvider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var requestContent = await HttpContentToString(request.Content);
            Debug.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} REQUEST [CorrelationId: {_executionContextValueProvider.GetCorrelationId()}] {request.Method} {request.RequestUri.OriginalString} {requestContent}");

            var response = await base.SendAsync(request, cancellationToken);

            var responseContent = await HttpContentToString(response.Content);
            Debug.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} RESPONSE [CorrelationId: {_executionContextValueProvider.GetCorrelationId()}] {response.StatusCode} {responseContent}");

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
