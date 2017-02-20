
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;




namespace Temp.Utilities
{
    public class MyExceptionHandler : ExceptionHandler
    {
        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            return base.HandleAsync(context, cancellationToken);
        }
    }
}