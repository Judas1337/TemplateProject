using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace Temp.Utilities
{
    public class MyExceptionLogger : ExceptionLogger
    {
        public override Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            return base.LogAsync(context, cancellationToken);
        }
    }
}