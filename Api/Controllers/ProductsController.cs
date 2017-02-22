using System.Collections.Generic;
using System.Web.Http;
using Api.Logic;
using Api.Models;
using Api.Utilities;
using Autofac.Integration.WebApi;

namespace Api.Controllers
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
            InputGuard.ThrowArgumentExceptionIfNegativeValue(nameof(id), id);
            return _productLogic.GetProduct(id);
        }

        [HttpPost]
        public Product AddProduct(Product product)
        {
            InputGuard.ThrowArgumentNullExceptionIfNull(nameof(product), product);
            return _productLogic.AddProduct(product);
        }
    }
}
