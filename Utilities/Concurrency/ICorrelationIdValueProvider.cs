namespace WebApiTemplateProject.Utilities.Concurrency
{
    public interface ICorrelationIdValueProvider<T>
    {
        T GetCorrelationId();
        void SetCorrelationId(T correlationId);
    }
}
