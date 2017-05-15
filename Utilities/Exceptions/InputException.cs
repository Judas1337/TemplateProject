using System;

namespace TemplateProject.Utilities.Exceptions
{
    public class InputException : SemanticException
    {
        protected InputException() : base() { }
        protected InputException(string message) : base(message) { }
        protected InputException(string message, Exception innerException) : base(message, innerException) { }
    }
}
