using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTemplateProject.Utilities.Exceptions
{
    public abstract class SemanticException : Exception
    {
        protected SemanticException() : base() { }
        protected SemanticException(string message) : base(message) { }
        protected SemanticException(string message, Exception innerException) : base(message, innerException) { }
    }
}
