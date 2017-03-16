using System;
using System.Collections.Generic;
using System.Web.Http;
using WebApiTemplateProject.Api.Logic;
using WebApiTemplateProject.Api.Models;
using WebApiTemplateProject.Utilities.Guard;

namespace WebApiTemplateProject.Api.Controllers
{
    [RoutePrefix("api/v1/Products")]
    public class ProductsController : ApiController
    {
        private readonly IProductLogic _productLogic;

        public ProductsController(IProductLogic productLogic)
        {
            _productLogic = productLogic;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<Product> GetAllProducts()
        {
            return _productLogic.GetAllProducts();
        }

        [HttpGet]
        [Route("{id}")]
        public Product GetProduct(int id)
        {
            NetInputGuard.ThrowArgumentExceptionIfNegativeValue(nameof(id), id);
            return _productLogic.GetProduct(id);
        }

        [HttpPost]
        [Route("")]
        public Product CreateProduct(Product product)
        {
            NetInputGuard.ThrowArgumentNullExceptionIfNull(nameof(product), product);
            return _productLogic.CreateProduct(product);
        }
    }
}
