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
        private IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        [HttpGet]
        public Product GetProduct(int id)
        {
            return _productRepository.GetProduct(id);
        }
    }
}
