namespace WebApiTemplateProject.Utilities.Concurrency
{
    public interface IExecutionContextValueProvider
    {
        string GetCorrelationId();
        void SetCorrelationId(string correlationId);
        object GetData(string name);
        void SetData(string name, object value);
    }
}