using System.Collections.Generic;
using Api.Models;

namespace Api.Logic
{
    public interface IProductLogic
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProduct(int id);
        Product AddProduct(Product product);
    }
}
