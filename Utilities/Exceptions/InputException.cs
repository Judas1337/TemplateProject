using System;

namespace TemplateProject.Utilities.Exceptions
{
    public class InputException : SemanticException
    {
        public InputException() : base() { }
        public InputException(string message) : base(message) { }
        public InputException(string message, Exception innerException) : base(message, innerException) { }
    }
}
