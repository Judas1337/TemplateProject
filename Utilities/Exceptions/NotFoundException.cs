using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateProject.Utilities.Exceptions
{
    public class NotFoundException : SemanticException
    {
        protected NotFoundException() : base() { }
        protected NotFoundException(string message) : base(message) { }
        protected NotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
