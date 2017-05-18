using System;

namespace TemplateProject.Utilities.Exceptions
{
    public class ConflictException : SemanticException
    {
        public ConflictException() : base() { }
        public ConflictException(string message) : base(message) { }
        public ConflictException(string message, Exception innerException) : base(message, innerException) { }
    }
}
