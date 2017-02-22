using System;
using System.Collections.Generic;
using Api.Models;

namespace Api.DataAccess
{
    public interface IProductRepository : IDisposable
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProduct(int id);
        Product AddProduct(Product product);
    }
}
