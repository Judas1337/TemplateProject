using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using WebApiTemplateProject.Utilities.Concurrency;

namespace WebApiTemplateProject.Utilities.Logger
{
    public class GlobalExceptionLogger : ExceptionLogger
    {
        private readonly ICorrelationIdValueProvider<Guid?> _correlationIdValueProvider;

        public GlobalExceptionLogger(ICorrelationIdValueProvider<Guid?> correlationIdValueProvider)
        {
            _correlationIdValueProvider = correlationIdValueProvider;
        }
       
        public override Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            Debug.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [CorrelationId: {_correlationIdValueProvider.GetCorrelationId()}] UNHANDLED EXCEPTION {context.Exception.Message}");
            return base.LogAsync(context, cancellationToken);
        }
    }
}