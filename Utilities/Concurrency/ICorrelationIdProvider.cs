namespace TemplateProject.Utilities.Concurrency
{
    public interface ICorrelationIdProvider<T>
    {
        T GetCorrelationId();
        void SetCorrelationId(T correlationId);
    }
}
