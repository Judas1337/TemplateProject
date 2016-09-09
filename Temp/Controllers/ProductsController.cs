using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac.Integration.WebApi;
using Temp.Models;

namespace Temp.Controllers
{
    [AutofacControllerConfiguration]
    public class ProductsController : ApiController
    {
        private IProductLogic _productLogic;

        public ProductsController(IProductLogic productLogic)
        {
            _productLogic = productLogic;
        }

        [HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            return _productLogic.GetAllProducts();
        }

        [HttpGet]
        public Product GetProduct(int id)
        {
            return _productLogic.GetProduct(id);
        }
    }
}
