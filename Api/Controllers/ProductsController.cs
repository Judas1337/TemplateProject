using System.Collections.Generic;
using System.Web.Http;
using Autofac.Integration.WebApi;
using WebApiTemplateProject.Api.Logic;
using WebApiTemplateProject.Api.Models;
using WebApiTemplateProject.Api.Utilities;

namespace WebApiTemplateProject.Api.Controllers
{
    [AutofacControllerConfiguration]
    public class ProductsController : ApiController
    {
        private readonly IProductLogic _productLogic;

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
