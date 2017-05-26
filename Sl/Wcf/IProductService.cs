using System.ServiceModel;

namespace TemplateProject.Sl.Wcf
{
    [ServiceContract]
    public interface IProductService
    {
        [OperationContract]
        string GetData(int value);
    }
}
