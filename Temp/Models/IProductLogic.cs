using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temp.Models
{
    public interface IProductLogic
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProduct(int id);
    }
}
