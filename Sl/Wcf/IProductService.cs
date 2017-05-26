using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using TemplateProject.Sl.Wcf.Model;
using TemplateProject.Sl.Wcf.Model.Exceptions;

namespace TemplateProject.Sl.Wcf
{
    [ServiceContract]
    public interface IProductService
    {
        [OperationContract]      
        [FaultContract(typeof(InputFault))]
        [FaultContract(typeof(GenericFault))]
        Task<IEnumerable<Product>> GetAllProducts();

        [OperationContract]     
        [FaultContract(typeof(NotFoundFault))]
        [FaultContract(typeof(InputFault))]
        [FaultContract(typeof(GenericFault))]
        Task<Product> GetProduct(int id);

        [OperationContract]
        [FaultContract(typeof(NotFoundFault))]
        [FaultContract(typeof(InputFault))]
        [FaultContract(typeof(GenericFault))]
        Task<Product> CreateProduct(Product product);

        [OperationContract]
        [FaultContract(typeof(NotFoundFault))]
        [FaultContract(typeof(InputFault))]
        [FaultContract(typeof(GenericFault))]
        Task<Product> UpdateProduct(Product product);

        [OperationContract]
        [FaultContract(typeof(NotFoundFault))]
        [FaultContract(typeof(InputFault))]
        [FaultContract(typeof(GenericFault))]
        Task<Product> DeleteProduct(int id);
    }
}
