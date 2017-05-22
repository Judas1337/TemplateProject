using System;

namespace TemplateProject.Bll.Contract.Bll.Model.Exceptions
{
    public class InputException : SemanticException
    {
        public InputException() : base() { }
        public InputException(string message) : base(message) { }
        public InputException(string message, Exception innerException) : base(message, innerException) { }
    }
}
