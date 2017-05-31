using System.Collections.Generic;
using System.Threading.Tasks;
using TemplateProject.Bll.Contract.Bll.Interface;
using TemplateProject.Bll.Contract.Bll.Model;
using TemplateProject.Bll.Contract.Dal;

namespace TemplateProject.Bll
{
    public class ProductLogic : IProductLogic
    {
        private readonly IProductRepository _productRepository;

        public ProductLogic(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productRepository.GetAllProducts().ConfigureAwait(false);
        }

        public async Task<Product> GetProduct(int id)
        {
            var product = await _productRepository.GetProduct(id).ConfigureAwait(false);
            return product;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            ThrowInputExceptionIfInvalidProduct(product);

            return await _productRepository.CreateProduct(product).ConfigureAwait(false);
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            ThrowInputExceptionIfInvalidProduct(product);

            return await _productRepository.UpdateProduct(product).ConfigureAwait(false);
        }

        public async Task<Product> DeleteProduct(int id)
        {
            return await _productRepository.DeleteProduct(id).ConfigureAwait(false);
        }

        #region HelperMethods
        private void ThrowInputExceptionIfInvalidProduct(Product product)
        {
            SemanticInputGuard.ThrowInputExceptionIfNull(nameof(product), product);
            SemanticInputGuard.ThrowInputExceptionIfNegativeValue(nameof(product.Id), product.Id);
            SemanticInputGuard.ThrowInputExceptionIfNotMatchesRegex(nameof(product.Name), product.Name, "^[a-zA-Z]+$");
            SemanticInputGuard.ThrowInputExceptionIfNotMatchesRegex(nameof(product.Category), product.Category, "^[a-zA-Z]+$");
            SemanticInputGuard.ThrowInputExceptionIfNegativeValue(nameof(product.Price), product.Price);
            SemanticInputGuard.ThrowInputExceptionIfDefaultValue(nameof(product.DateAdded), product.DateAdded);
        }
        #endregion
    }
}