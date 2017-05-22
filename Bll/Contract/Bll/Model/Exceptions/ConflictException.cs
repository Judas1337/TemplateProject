using System;

namespace TemplateProject.Bll.Contract.Bll.Model.Exceptions
{
    public class ConflictException : SemanticException
    {
        public ConflictException() : base() { }
        public ConflictException(string message) : base(message) { }
        public ConflictException(string message, Exception innerException) : base(message, innerException) { }
    }
}
