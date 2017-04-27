﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Debug.WriteLine($"REQUEST [CorrelationId: {_executionContextValueProvider.GetCorrelationId()}] {request.Method} {request.RequestUri.OriginalString} {requestContent}");

            var response = await base.SendAsync(request, cancellationToken);

            var responseContent = await HttpContentToString(response.Content);
            Debug.WriteLine($"RESPONSE [CorrelationId: {_executionContextValueProvider.GetCorrelationId()}] {response.StatusCode} {responseContent}\n");

            return response;
        }

        private async Task<string> HttpContentToString(HttpContent content)
        {
            if (content == null) return null;
            await content.LoadIntoBufferAsync();
            var text = await content.ReadAsStringAsync();
            return text;
        }
    }
}