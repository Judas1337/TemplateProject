using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApiTemplateProject.Api.Controllers;
using WebApiTemplateProject.Api.DataAccess;
using WebApiTemplateProject.Api.Logic;
using WebApiTemplateProject.RegressionTest.Helpers;

namespace WebApiTemplateProject.RegressionTest.Product
{
    [TestClass]
    public class ProductsControllerTests
    {
        private ProductsController _controller;
        private Mock<IProductRepository> _productRepository;
        private Api.Models.Product _expectedProduct;

        [TestInitialize]
        public void Initialize()
        {
            _expectedProduct = ProductFactory.CreateProduct();
            _productRepository = new Mock<IProductRepository>();
            _controller = new ProductsController(new ProductLogic(_productRepository.Object));
        }

        #region GetProduct()
        [TestMethod]
        public async Task GetProduct()
        {
            _productRepository.Setup(mock => mock.GetProduct(_expectedProduct.Id)).ReturnsAsync(_expectedProduct);
            var actualProduct = await _controller.GetProduct(_expectedProduct.Id);
            AssertProductsAreEqual(_expectedProduct, actualProduct);
        }
        #endregion

        #region GetAllProduct()
        [TestMethod]
        public async Task GetAllProducts()
        {
            var expectedProducts = ProductFactory.CreateProducts(3);
            _productRepository.Setup(mockRepo => mockRepo.GetAllProducts()).ReturnsAsync(expectedProducts);
            var actualProducts = await _controller.GetAllProducts();
            AssertProductsAreEqual(expectedProducts.ToList(), actualProducts.ToList());
        }
        #endregion

        #region UpdateProduct()
        [TestMethod]
        public async Task UpdateProduct()
        {
            _productRepository.Setup(mockRepo => mockRepo.UpdateProduct(_expectedProduct)).ReturnsAsync(_expectedProduct);
            ModelStateValidator.AssertModelIsValid(_expectedProduct);
            var actualProduct = await _controller.UpdateProduct(_expectedProduct);

            AssertProductsAreEqual(_expectedProduct, actualProduct);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task UpdateProductNullInput()
        {
            await _controller.CreateProduct(null);
        }
        #endregion

        #region CreateProduct()
        [TestMethod]
        public async Task CreateProduct()
        {
            _productRepository.Setup(mockRepo => mockRepo.CreateProduct(_expectedProduct)).ReturnsAsync(_expectedProduct);
            ModelStateValidator.AssertModelIsValid(_expectedProduct);
            var actualProduct = await _controller.CreateProduct(_expectedProduct);

            AssertProductsAreEqual(_expectedProduct, actualProduct);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task CreateProductNullInput()
        {
            await _controller.CreateProduct(null);
        }
        #endregion

        #region DeleteProduct
        [TestMethod]
        public async Task DeleteProduct()
        {
            _productRepository.Setup(mock => mock.DeleteProduct(_expectedProduct.Id)).ReturnsAsync(_expectedProduct);
            var actualProduct = await _controller.DeleteProduct(_expectedProduct.Id);
            AssertProductsAreEqual(_expectedProduct, actualProduct);
        }
        #endregion

        #region AssertionMethods

        private void AssertProductsAreEqual(IList<Api.Models.Product> expected, IList<Api.Models.Product> actual)
        {
            if (expected == null) throw new ArgumentNullException(nameof(expected));
            if (actual == null) throw new ArgumentNullException(nameof(actual));

            Assert.AreEqual(expected.Count, expected.Count, "Number of products");

            for (var i = 0; i < expected.Count; i++)
            {
                AssertProductsAreEqual(expected.ElementAt(i), actual.ElementAt(i));
            }
        }

        private void AssertProductsAreEqual(Api.Models.Product expected, Api.Models.Product actual)
        {
            Assert.AreEqual(expected.Id, actual.Id, "Product Id");
            Assert.AreEqual(expected.Name, actual.Name, "Product name");
            Assert.AreEqual(expected.Category, actual.Category, "Product Category");
            Assert.AreEqual(expected.Price, actual.Price, "Product Price");
            Assert.AreEqual(expected.DateAdded, actual.DateAdded, "Product DateAdded");
        }
        #endregion
    }
}
