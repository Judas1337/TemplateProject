using System;

namespace TemplateProject.Utilities.Exceptions
{
    public class NotFoundException : SemanticException
    {
        protected NotFoundException() : base() { }
        protected NotFoundException(string message) : base(message) { }
        protected NotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
