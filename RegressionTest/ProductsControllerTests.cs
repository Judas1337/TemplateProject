using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApiTemplateProject.Api.Controllers;
using WebApiTemplateProject.Api.DataAccess;
using WebApiTemplateProject.Api.Logic;
using WebApiTemplateProject.Api.Models;

namespace WebApiTemplateProject.RegressionTest
{
    [TestClass]
    public class ProductsControllerTests
    {
        private ProductsController _controller;
        private Mock<IProductRepository> _productRepository;
        private Product _expectedProduct;

        [TestInitialize]
        public void Initialize()
        {
            _expectedProduct = CreateProduct();
            _productRepository = new Mock<IProductRepository>();
            _controller = new ProductsController(new ProductLogic(_productRepository.Object));
        }

        #region GetProduct()

        [TestMethod]
        public void GetProduct()
        {
            _productRepository.Setup(mock => mock.GetProduct(_expectedProduct.Id)).Returns(_expectedProduct);
            var actualProduct = _controller.GetProduct(_expectedProduct.Id);
            AssertProductsAreEqual(_expectedProduct, actualProduct);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetProductNegativeId()
        {
          _controller.GetProduct(-1);
        }
        #endregion

        #region GetAllProduct()
        [TestMethod]
        public void GetAllProducts()
        {
            var expectedProducts = CreateProducts(3);
            _productRepository.Setup(mockRepo => mockRepo.GetAllProducts()).Returns(expectedProducts);
            var actualProducts = _controller.GetAllProducts();
            AssertProductsAreEqual(expectedProducts, actualProducts);
        }
        #endregion

        #region AddProduct()
        [TestMethod]
        public void AddProduct()
        {
            _productRepository.Setup(mockRepo => mockRepo.AddProduct(_expectedProduct)).Returns(_expectedProduct);
            var actualProduct = _controller.AddProduct(_expectedProduct);

            AssertProductsAreEqual(_expectedProduct, actualProduct);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddProductNullInput()
        {
            _controller.AddProduct(null);
        }
        #endregion

        #region PrivateMethods

        #region AssertionMethods

        private void AssertProductsAreEqual(IEnumerable<Product> expected, IEnumerable<Product> actual)
        {
            Assert.AreEqual(expected.Count(), expected.Count(), "Number of products");

            for (int i = 0; i < expected.Count(); i++)
            {
                AssertProductsAreEqual(expected.ElementAt(i), actual.ElementAt(i));
            }
        }

        private void AssertProductsAreEqual(Product expected, Product actual)
        {
            Assert.AreEqual(expected.Id, actual.Id, "Product Id");
            Assert.AreEqual(expected.Name, actual.Name, "Product name");
            Assert.AreEqual(expected.Category, actual.Category, "Product Category");
            Assert.AreEqual(expected.Price, actual.Price, "Product Price");
            Assert.AreEqual(expected.DateAdded, actual.DateAdded, "Product DateAdded");
        }
        #endregion

        #region FactoryMethods

        private IList<Product> CreateProducts(int numberOfProducts)
        {
            IList<Product> products = new List<Product>();
            for (var i = 0; i < numberOfProducts; ++i)
            {
                products.Add(CreateProduct(i + 1));
            }
            return products;
        }


        private Product CreateProduct(int id = 1, string name = "mock name", string category = "mock category", decimal price = 123)
        {
            return new Product
            {
                Id = id,
                Name = name,
                Category = category,
                Price = price,
                DateAdded = new DateTime(2017, 02, 22)
            };
        }
        #endregion
        #endregion
    }
}
