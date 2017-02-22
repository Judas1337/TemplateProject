using System.Collections.Generic;
using WebApiTemplateProject.Api.Models;

namespace WebApiTemplateProject.Api.Logic
{
    public interface IProductLogic
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProduct(int id);
        Product AddProduct(Product product);
    }
}
