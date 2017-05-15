using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateProject.Utilities.Exceptions
{
    public class UnauthorizedException : SemanticException
    {
        protected UnauthorizedException() : base() { }
        protected UnauthorizedException(string message) : base(message) { }
        protected UnauthorizedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
