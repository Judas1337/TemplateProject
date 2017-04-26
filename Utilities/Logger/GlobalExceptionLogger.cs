using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using WebApiTemplateProject.Utilities.Concurrency;

namespace WebApiTemplateProject.Utilities.Logger
{
    public class GlobalExceptionLogger : ExceptionLogger
    {
        private readonly IExecutionContextValueProvider _executionContextValueProvider;

        public GlobalExceptionLogger(IExecutionContextValueProvider executionContextValueProvider)
        {
            _executionContextValueProvider = executionContextValueProvider;
        }
        public override Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            var correlationId = _executionContextValueProvider.GetCorrelationId();
            Debug.WriteLine($"ERROR_RESPONSE [CorrelationId: {correlationId}] {context.Exception.Message}");
            return base.LogAsync(context, cancellationToken);
        }
    }
}