using System;
using System.Collections.Generic;

namespace WebApiTemplateProject.RegressionTest.Product
{
    internal static class ProductFactory
    {
        public static IList<Api.Models.Product> CreateProducts(int numberOfProducts)
        {
            IList<Api.Models.Product> products = new List<Api.Models.Product>();
            for (var i = 0; i < numberOfProducts; ++i)
            {
                products.Add(CreateProduct(i + 1));
            }
            return products;
        }

        public static Api.Models.Product CreateProduct(int id = 1, string name = "mockName", string category = "mockCategory", decimal price = 123)
        {
            return new Api.Models.Product
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
