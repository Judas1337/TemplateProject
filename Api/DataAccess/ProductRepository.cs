using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Rest;
using WebApiTemplateProject.Api.Models;
using WebApiTemplateProject.Utilities.Guard;

namespace WebApiTemplateProject.Api.DataAccess
{
    public class ProductRepository : IProductRepository
    {
        private Product[] _products = new Product[]
          {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1, DateAdded = new DateTime(2016,1,13)},
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M, DateAdded = new DateTime(2015,12,1)},
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M,  DateAdded = new DateTime(2017,1,17)}
          };

        public Task<IEnumerable<Product>> GetAllProducts()
        {
            return Task.FromResult<IEnumerable<Product>>(_products);
        }

        public Task<Product> GetProduct(int id)
        {
            GenericInputGuard.ThrowExceptionIf<int,Exception>(nameof(id), id, (param)=> param == 500);
            var product = GetProductOrThrowHttpNotFound(id);

            return Task.FromResult(product);
        }

        public Task<Product> CreateProduct(Product product)
        {
            ThrowHttpBadRequestIfProductWithIdExists(product.Id);
            return Task.FromResult(product);
        }

        public Task<Product> UpdateProduct(Product product)
        {
            GetProductOrThrowHttpNotFound(product.Id);
            return Task.FromResult(product);
        }

        public Task<Product> DeleteProduct(int id)
        {
            return Task.FromResult(GetProductOrThrowHttpNotFound(id));
        }

        public void Dispose()
        {
            _products = null;
        }

        #region HelperMethods
        private Product GetProductOrThrowHttpNotFound(int productId)
        {
            var product = _products.FirstOrDefault((p) => p.Id == productId);
            if (product == null) throw new HttpOperationException
            {
                Response = new HttpResponseMessageWrapper(new HttpResponseMessage(HttpStatusCode.NotFound), $"Product with Id: {productId} not found"),
            };

            return product;
        }

        private void ThrowHttpBadRequestIfProductWithIdExists(int productId)
        {
            var product = _products.FirstOrDefault((p) => p.Id == productId);


            if (product != null) throw new HttpOperationException
            {
                Response = new HttpResponseMessageWrapper(new HttpResponseMessage(HttpStatusCode.BadRequest), $"Product with Id: {productId} already exists"),
            };
        }
        #endregion
    }
}