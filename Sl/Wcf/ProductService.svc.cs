using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TemplateProject.Bll.Contract.Bll.Interface;
using TemplateProject.Sl.Wcf.Model;

namespace TemplateProject.Sl.Wcf
{    
    // NOTE: In order to launch WCF Test Client for testing this service, please select ProductService.svc or ProductService.svc.cs at the Solution Explorer and start debugging.
    public class ProductService : IProductService
    {
        private readonly IProductLogic _productLogic;

        public ProductService(IProductLogic productLogic)
        {
            _productLogic = productLogic;
        }       

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var domainModelProducts = await _productLogic.GetAllProducts();
            var result = AutoMapper.Mapper.Map<IEnumerable<Product>>(domainModelProducts);

            return result;
        }        

        public async Task<Product> GetProduct(int id)
        {
            var domainModelProduct = await _productLogic.GetProduct(id);
            var result = AutoMapper.Mapper.Map<Product>(domainModelProduct);

            return result;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            var domainModelProduct = AutoMapper.Mapper.Map<Bll.Contract.Bll.Model.Product>(product);
            domainModelProduct = await _productLogic.CreateProduct(domainModelProduct);
            var result = AutoMapper.Mapper.Map<Product>(domainModelProduct);

            return result;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var domainModelProduct = AutoMapper.Mapper.Map<Bll.Contract.Bll.Model.Product>(product);
            domainModelProduct = await _productLogic.UpdateProduct(domainModelProduct);
            var result = AutoMapper.Mapper.Map<Product>(domainModelProduct);

            return result;
        }

        public async Task<Product> DeleteProduct(int id)
        {
            var domainModelProduct = await _productLogic.DeleteProduct(id);
            var result = AutoMapper.Mapper.Map<Product>(domainModelProduct);

            return result;
        }
    }
}
