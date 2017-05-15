using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateProject.Utilities.Exceptions
{
    public class ConflictException : SemanticException
    {
        protected ConflictException() : base() { }
        protected ConflictException(string message) : base(message) { }
        protected ConflictException(string message, Exception innerException) : base(message, innerException) { }
    }
}
