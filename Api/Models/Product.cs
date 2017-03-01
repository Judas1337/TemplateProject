using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiTemplateProject.Api.Models
{
    public class Product
    {     
        [Range(0, int.MaxValue, ErrorMessage = "Id must be a positive value")]
        public int Id { get; set; }

        [Required]
        [RegularExpression("[a-zA-Z]+")]
        public string Name { get; set; }

        [Required]
        [RegularExpression("[a-zA-Z]+")]
        public string Category { get; set; }
      
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Id must be a positive value")]
        public decimal Price { get; set; }
        
        public DateTime DateAdded { get; set; }
    }
}