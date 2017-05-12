using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TemplateProject.Bll.Contract.Bll.Model;

namespace TemplateProject.Bll.Contract.Dal.Interface
{
    public interface IProductRepository : IDisposable
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProduct(int id);
        Task<Product> CreateProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<Product> DeleteProduct(int id);
    }
}
