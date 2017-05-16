using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using TemplateProject.Utilities.Concurrency;

namespace TemplateProject.Sl.WebApi.Logger
{
    public class GlobalExceptionLogger : ExceptionLogger
    {
        private readonly ICorrelationIdProvider<Guid?> _correlationIdProvider;

        public GlobalExceptionLogger(ICorrelationIdProvider<Guid?> correlationIdProvider)
        {
            _correlationIdProvider = correlationIdProvider;
        }
       
        public override Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            Debug.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [CorrelationId: {_correlationIdProvider.GetCorrelationId()}] UNHANDLED EXCEPTION {context.Exception.Message}");
            return base.LogAsync(context, cancellationToken);
        }
    }
}