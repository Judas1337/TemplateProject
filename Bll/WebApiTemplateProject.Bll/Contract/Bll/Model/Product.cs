using System;

namespace WebApiTemplateProject.Bll.Contract.Bll.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset DateAdded { get; set; }
    }
}