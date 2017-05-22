using System;

namespace TemplateProject.Bll.Contract.Bll.Model.Exceptions
{
    public class UnauthorizedException : SemanticException
    {
        public UnauthorizedException() : base() { }
        public UnauthorizedException(string message) : base(message) { }
        public UnauthorizedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
