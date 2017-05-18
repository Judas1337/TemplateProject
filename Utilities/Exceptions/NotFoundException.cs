using System;

namespace TemplateProject.Utilities.Exceptions
{
    public class NotFoundException : SemanticException
    {
        public NotFoundException() : base() { }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
