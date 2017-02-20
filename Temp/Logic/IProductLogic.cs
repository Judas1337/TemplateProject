using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temp.Models;

namespace Temp.Logic
{
    public interface IProductLogic
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProduct(int id);
    }
}
