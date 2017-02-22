using System.Collections.Generic;
using WebApiTemplateProject.Api.DataAccess;
using WebApiTemplateProject.Api.Models;

namespace WebApiTemplateProject.Api.Logic
{
    public class ProductLogic : IProductLogic
    {
        private readonly IProductRepository _productRepository;

        public ProductLogic(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        public Product GetProduct(int id)
        {
            var product =_productRepository.GetProduct(id);
            product.Category += " LogicHandled";
            return product;
        }

        public Product AddProduct(Product product)
        {
            return _productRepository.AddProduct(product);
        }
    }
}