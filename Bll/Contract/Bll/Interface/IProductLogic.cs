﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TemplateProject.Bll.Contract.Bll.Model;

namespace TemplateProject.Bll.Contract.Bll.Interface
{
    public interface IProductLogic
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProduct(int id);
        Task<Product> CreateProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<Product> DeleteProduct(int id);
    }
}
