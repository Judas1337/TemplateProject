using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using TemplateProject.Sl.Wcf.Model;

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
