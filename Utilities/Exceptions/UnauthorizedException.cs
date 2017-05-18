using System;

namespace TemplateProject.Utilities.Exceptions
{
    public class UnauthorizedException : SemanticException
    {
        public UnauthorizedException() : base() { }
        public UnauthorizedException(string message) : base(message) { }
        public UnauthorizedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
