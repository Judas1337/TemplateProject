using System;

namespace TemplateProject.Bll.Contract.Bll.Model.Exceptions
{
    public class NotFoundException : SemanticException
    {
        public NotFoundException() : base() { }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
