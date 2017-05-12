namespace TemplateProject.Utilities.Exceptions
{
    public abstract class SemanticException : System.Exception
    {
        protected SemanticException() : base() { }
        protected SemanticException(string message) : base(message) { }
        protected SemanticException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}
