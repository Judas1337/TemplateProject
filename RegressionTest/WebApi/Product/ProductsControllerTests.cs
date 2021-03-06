﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TemplateProject.Bll.Contract.Bll.Interface;
using TemplateProject.RegressionTest.WebApi.Helpers;
using TemplateProject.Sl.WebApi.App_Start;
using TemplateProject.Sl.WebApi.Controllers;


namespace TemplateProject.RegressionTest.WebApi.Product
{
    [TestClass]
    public class ProductsControllerTests
    {
        private ProductsController _controller;
        private Mock<IProductLogic> _productLogic;
        private Sl.WebApi.Model.Product _expectedProduct;
        private Bll.Contract.Bll.Model.Product _domainProduct;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            AutomapperConfig.RegisterMappings();
        }

        [TestInitialize]
        public void Initialize()
        {
            _expectedProduct = ProductFactory.CreateProduct();
            _domainProduct = AutoMapper.Mapper.Map<Bll.Contract.Bll.Model.Product>(_expectedProduct);
           
            _productLogic = new Mock<IProductLogic>();
            _controller = new ProductsController(_productLogic.Object);
        }

        #region GetProduct()
        [TestMethod]
        public async Task GetProduct()
        {
            _productLogic.Setup(mock => mock.GetProduct(_domainProduct.Id)).ReturnsAsync(_domainProduct);
            var actualProduct = await _controller.GetProduct(_expectedProduct.Id);
            AssertProductsAreEqual(_expectedProduct, actualProduct);
        }
        #endregion

        #region GetAllProduct()
        [TestMethod]
        public async Task GetAllProducts()
        {
            var expectedProducts = ProductFactory.CreateProducts(3);
            var domainProducts = AutoMapper.Mapper.Map<List<Bll.Contract.Bll.Model.Product>>(expectedProducts);
            _productLogic.Setup(mockRepo => mockRepo.GetAllProducts()).ReturnsAsync(domainProducts);
            var actualProducts = await _controller.GetAllProducts();
            AssertProductsAreEqual(expectedProducts.ToList(), actualProducts.ToList());
        }
        #endregion

        #region UpdateProduct()
        [TestMethod]
        public async Task UpdateProduct()
        {
            _productLogic.Setup(mockRepo => mockRepo.UpdateProduct(It.IsNotNull<Bll.Contract.Bll.Model.Product>())).ReturnsAsync(_domainProduct);
            ModelStateValidator.AssertModelIsValid(_expectedProduct);
            var actualProduct = await _controller.UpdateProduct(_expectedProduct);

            AssertProductsAreEqual(_expectedProduct, actualProduct);
        }
        #endregion

        #region CreateProduct()
        [TestMethod]
        public async Task CreateProduct()
        {
            _productLogic.Setup(mockRepo => mockRepo.CreateProduct(It.IsNotNull<Bll.Contract.Bll.Model.Product>())).ReturnsAsync(_domainProduct);
            ModelStateValidator.AssertModelIsValid(_expectedProduct);
            var actualProduct = await _controller.CreateProduct(_expectedProduct);

            AssertProductsAreEqual(_expectedProduct, actualProduct);
        }
        #endregion

        #region DeleteProduct
        [TestMethod]
        public async Task DeleteProduct()
        {
            _productLogic.Setup(mock => mock.DeleteProduct(_domainProduct.Id)).ReturnsAsync(_domainProduct);
            var actualProduct = await _controller.DeleteProduct(_expectedProduct.Id);
            AssertProductsAreEqual(_expectedProduct, actualProduct);
        }
        #endregion

        #region AssertionMethods

        private void AssertProductsAreEqual(IList<Sl.WebApi.Model.Product> expected, IList<Sl.WebApi.Model.Product> actual)
        {
            if (expected == null) throw new ArgumentNullException(nameof(expected));
            if (actual == null) throw new ArgumentNullException(nameof(actual));

            Assert.AreEqual(expected.Count, expected.Count, "Number of products");

            for (var i = 0; i < expected.Count; i++)
            {
                AssertProductsAreEqual(expected.ElementAt(i), actual.ElementAt(i));
            }
        }

        private void AssertProductsAreEqual(Sl.WebApi.Model.Product expected, Sl.WebApi.Model.Product actual)
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
