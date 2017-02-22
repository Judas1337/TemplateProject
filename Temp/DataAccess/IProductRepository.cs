using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temp.Models;

namespace Temp.DataAccess
{
    public interface IProductRepository : IDisposable
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProduct(int id);
        Product AddProduct(Product product);
    }
}
