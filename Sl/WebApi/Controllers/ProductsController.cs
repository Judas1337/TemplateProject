using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using TemplateProject.Bll.Contract.Bll.Interface;
using TemplateProject.Sl.WebApi.Model;
using TemplateProject.Utilities.Guard;

namespace TemplateProject.Sl.WebApi.Controllers
{
    /// <summary>
    /// ApiController for Product that does inputcontrol. Logic is separated into another layer. 
    /// </summary>
    [RoutePrefix("api/v1/Products")]
    public class ProductsController : ApiController
    {
        private readonly IProductLogic _productLogic;

        /// <summary>
        /// Constructor that takes a logic layer for product. 
        /// </summary>
        /// <param name="productLogic">Dependency injected logic layer</param>
        public ProductsController(IProductLogic productLogic)
        {
            _productLogic = productLogic;
        }

        /// <summary>
        /// Retrieves all products that exists.
        /// </summary>
        /// <returns>All products</returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var domainModelProduct = await _productLogic.GetAllProducts();
            var result = AutoMapper.Mapper.Map<IEnumerable<Product>>(domainModelProduct);

            return result;
        }

        /// <summary>
        /// Retrieves a Product that has the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The product with the specified <paramref name="id"/></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Product with the specified <paramref name="id"/> not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("{id}")]
        public async Task<Product> GetProduct(int id)
        {
            var domainModelProduct = await _productLogic.GetProduct(id);
            var result = AutoMapper.Mapper.Map<Product>(domainModelProduct);

            return result;
        }

        /// <summary>
        /// Creates a Product with the values specified in <paramref name="product"/>
        /// </summary>
        /// <param name="product"></param>
        /// <returns>The product that was created</returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("")]
        public async Task<Product> CreateProduct(Product product)
        {
            NetInputGuard.ThrowArgumentNullExceptionIfNull(nameof(product), product);

            var domainModelProduct = AutoMapper.Mapper.Map<Bll.Contract.Bll.Model.Product>(product);
            domainModelProduct = await _productLogic.CreateProduct(domainModelProduct);
            var result = AutoMapper.Mapper.Map<Product>(domainModelProduct);

            return result;
        }

        /// <summary>
        /// Updates a Product with the values specified in <paramref name="product"/>
        /// </summary>
        /// <param name="product"></param>
        /// <returns>The product that was updated</returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Product with specified id in <paramref name="product"/> not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("")]
        public async Task<Product> UpdateProduct(Product product)
        {
            NetInputGuard.ThrowArgumentNullExceptionIfNull(nameof(product), product);

            var domainModelProduct = AutoMapper.Mapper.Map<Bll.Contract.Bll.Model.Product>(product);
            domainModelProduct = await _productLogic.UpdateProduct(domainModelProduct);
            var result = AutoMapper.Mapper.Map<Product>(domainModelProduct);

            return result;
        }

        /// <summary>
        /// Deletes the Product with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id of the Product that is supposed to be deleted</param>
        /// <returns>The product that was deleted</returns>
        /// <response code="400">Bad request</response>
        /// <response code="404">Product with <paramref name="id"/> not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Route("")]
        public async Task<Product> DeleteProduct(int id)
        {
            var domainModelProduct = await _productLogic.DeleteProduct(id);
            var result = AutoMapper.Mapper.Map<Product>(domainModelProduct);

            return result;
        }
    }
}
