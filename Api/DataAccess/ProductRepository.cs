﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiTemplateProject.Api.Models;

namespace WebApiTemplateProject.Api.DataAccess
{
    public class ProductRepository : IProductRepository
    {
        private Product[] _products = new Product[]
          {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
          };

        public IEnumerable<Product> GetAllProducts()
        {
            return _products;
        }

        public Product GetProduct(int id)
        {
            if (id == 500) throw new Exception();

            var product = _products.FirstOrDefault((p) => p.Id == id);
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
            _products = null;
        }
    }
}