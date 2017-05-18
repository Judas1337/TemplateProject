namespace TemplateProject.Utilities.Concurrency
{
    public interface ICorrelationIdProvider
    {
        string GetCorrelationId();
        void SetCorrelationId(string correlationId);
    }
}
