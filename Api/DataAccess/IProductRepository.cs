using System;
using System.Collections.Generic;
using WebApiTemplateProject.Api.Models;

namespace WebApiTemplateProject.Api.DataAccess
{
    public interface IProductRepository : IDisposable
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProduct(int id);
        Product AddProduct(Product product);
    }
}
