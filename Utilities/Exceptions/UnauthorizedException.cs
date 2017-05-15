using System;

namespace TemplateProject.Utilities.Exceptions
{
    public class UnauthorizedException : SemanticException
    {
        protected UnauthorizedException() : base() { }
        protected UnauthorizedException(string message) : base(message) { }
        protected UnauthorizedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
