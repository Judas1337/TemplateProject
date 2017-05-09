using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApiTemplateProject.RegressionTest.Helpers;

namespace WebApiTemplateProject.RegressionTest.Product
{
    [TestClass]
    public class ProductModelValidationTests
    {
        private Api.Models.Product _expectedProduct;

        [TestInitialize]
        public void Initialize()
        {
            _expectedProduct = ProductFactory.CreateProduct();
        }

        [TestMethod]
        public void ProductNegativeId()
        {
            _expectedProduct.Id = -1;
            ModelStateValidator.AssertFieldIsInvalid(_expectedProduct, "Id");
        }

        [TestMethod]
        public void ProductNegativePrice()
        {
            _expectedProduct.Price = -1;
            ModelStateValidator.AssertFieldIsInvalid(_expectedProduct, "Price");
        }

        [TestMethod]
        public void ProductNameWithNumbers()
        {
            _expectedProduct.Name = _expectedProduct.Name + -1;
            ModelStateValidator.AssertFieldIsInvalid(_expectedProduct, "Name");
        }

        [TestMethod]
        public void ProductCategoryNameWithNumbers()
        {
            _expectedProduct.Category = _expectedProduct.Category + -1;
            ModelStateValidator.AssertFieldIsInvalid(_expectedProduct, "Category");
        }
    }
}
