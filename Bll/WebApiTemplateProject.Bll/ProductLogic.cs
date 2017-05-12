﻿using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiTemplateProject.Bll.Contract.Bll.Interface;
using WebApiTemplateProject.Bll.Contract.Bll.Model;
using WebApiTemplateProject.Bll.Contract.Dal.Interface;

namespace WebApiTemplateProject.Bll
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
            return await _productRepository.CreateProduct(product).ConfigureAwait(false);
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            return await _productRepository.UpdateProduct(product).ConfigureAwait(false);
        }

        public async Task<Product> DeleteProduct(int id)
        {
            return await _productRepository.DeleteProduct(id).ConfigureAwait(false);
        }
    }
}