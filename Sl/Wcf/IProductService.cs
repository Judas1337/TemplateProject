using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using TemplateProject.Bll.Contract.Bll.Model.Exceptions;
using TemplateProject.Sl.Wcf.Model;
using TemplateProject.Sl.Wcf.Model.Exceptions;

namespace TemplateProject.Sl.Wcf
{
    [ServiceContract]
    public interface IProductService
    {
        [OperationContract]
        Task<IEnumerable<Product>> GetAllProducts();

        [OperationContract]      
        Task<Product> GetProduct(int id);

        [OperationContract]
        Task<Product> CreateProduct(Product product);

        [OperationContract]
        Task<Product> UpdateProduct(Product product);

        [OperationContract]
        Task<Product> DeleteProduct(int id);
    }
}
