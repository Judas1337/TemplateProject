﻿using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TemplateProject.Utilities.Concurrency;

namespace TemplateProject.Sl.WebApi.MessageHandler
{
    /// <summary>
    /// Logs incoming requests and outgoing responses.
    /// </summary>
    public class MessageLoggingHandler : DelegatingHandler
    {
        private readonly ICorrelationIdProvider _correlationIdProvider;

        public MessageLoggingHandler(ICorrelationIdProvider correlationIdProvider)
        {
            _correlationIdProvider = correlationIdProvider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var timeAtRequest = DateTimeOffset.Now;
            var requestContent = await HttpContentToString(request.Content);
            request.Headers.Date = timeAtRequest;
            Debug.WriteLine($"{timeAtRequest:yyyy-MM-dd HH:mm:ss.fff} [CorrelationId: {_correlationIdProvider.GetCorrelationId()}] REQUEST {request.Method} {request.RequestUri.OriginalString} {requestContent}");

            var response = await base.SendAsync(request, cancellationToken);
           
            var responseContent = await HttpContentToString(response.Content);
            var timeAtResponse = DateTimeOffset.Now;
            var timeTaken = GetTimeTakeForRequestInMilliseconds(timeAtRequest, timeAtResponse);
            Debug.WriteLine($"{timeAtResponse:yyyy-MM-dd HH:mm:ss.fff} [CorrelationId: {_correlationIdProvider.GetCorrelationId()}] RESPONSE {response.StatusCode} {timeTaken}ms {responseContent}");

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

        private static string GetTimeTakeForRequestInMilliseconds(DateTimeOffset requestAt, DateTimeOffset timeAtResponse)
        {
            var diff = timeAtResponse - requestAt;
            var result = (int)diff.TotalMilliseconds;
            return $"{result}";
        }
    }
}
