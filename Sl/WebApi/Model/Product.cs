using System;
using System.ComponentModel.DataAnnotations;

namespace TemplateProject.Sl.WebApi.Model
{
    public class Product
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [RegularExpression("[a-zA-Z]+")]
        public string Name { get; set; }

        [Required]
        [RegularExpression("[a-zA-Z]+")]
        public string Category { get; set; }
      
        [Required]
        [Range(0, double.PositiveInfinity)]
        public double Price { get; set; }
        
        [Required]
        public DateTimeOffset DateAdded { get; set; }
    }
}