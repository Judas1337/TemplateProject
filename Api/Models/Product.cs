using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiTemplateProject.Api.Models
{
    public class Product
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [RegularExpression("[a-zA-Z]+")]
        public string Name { get; set; }

        [Required]
        [RegularExpression("[a-zA-Z]+")]
        public string Category { get; set; }
      
        [Required]
        [Range(1, int.MaxValue)]
        public decimal Price { get; set; }
        
        [Required]
        public DateTime DateAdded { get; set; }
    }
}