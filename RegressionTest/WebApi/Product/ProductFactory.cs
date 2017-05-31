using System;
using System.Collections.Generic;

namespace TemplateProject.RegressionTest.WebApi.Product
{
    internal static class ProductFactory
    {
        public static IList<Sl.WebApi.Model.Product> CreateProducts(int numberOfProducts)
        {
            IList<Sl.WebApi.Model.Product> products = new List<Sl.WebApi.Model.Product>();
            for (var i = 0; i < numberOfProducts; ++i)
            {
                products.Add(CreateProduct(i + 1));
            }
            return products;
        }

        public static Sl.WebApi.Model.Product CreateProduct(int id = 1, string name = "mockName", string category = "mockCategory", double price = 123)
        {
            return new Sl.WebApi.Model.Product
            {
                Id = id,
                Name = name,
                Category = category,
                Price = price,
                DateAdded = new DateTime(2017, 02, 22)
            };
        }
    }
}
