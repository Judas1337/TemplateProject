using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Temp.Controllers;
using Temp.Models;

namespace Temp.Tests
{
    [TestClass]
    public class ProductsControllerTests
    {
        [TestMethod]
        public void GetProductOne()
        {
            var expectedProduct = new Product {Id = 1, Name = "Tomato Soup", Category = "Groceries LogicHandled", Price = 1};

            ProductsController controller = new ProductsController(new ProductLogic(new ProductRepository()));
            var actualProduct = controller.GetProduct(1);

            Assert.AreEqual(expectedProduct.Id, actualProduct.Id, "Product Id");
            Assert.AreEqual(expectedProduct.Name, actualProduct.Name, "Product name");
            Assert.AreEqual(expectedProduct.Category, actualProduct.Category, "Product Category");
            Assert.AreEqual(expectedProduct.Price, actualProduct.Price, "Product Price");
        }

        public void GetProductOneMocked()
        {
            
        }


    }
}
