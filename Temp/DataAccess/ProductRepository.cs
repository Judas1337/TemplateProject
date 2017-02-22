using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Temp.Models;

namespace Temp.DataAccess
{
    public class ProductRepository : IProductRepository
    {
        Product[] products = new Product[]
          {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
          };

        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        public Product GetProduct(int id)
        {
            if (id == 500) throw new Exception();

            var product = products.FirstOrDefault((p) => p.Id == id);
            if(product == null) throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent($"Product with Id: {id} not found"),
                ReasonPhrase = "NotFound"
            });
            return product;
        }

        public Product AddProduct(Product product)
        {
            return product;
        }

        public void Dispose()
        {
            products = null;
        }
    }
}