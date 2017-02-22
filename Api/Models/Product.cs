using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Product
    {
        [Required]
        [Range(0, Double.MaxValue, ErrorMessage = "Id must be a positive value")]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }
      
        [Required]
        [Range(0, Double.MaxValue , ErrorMessage = "Price must be a positive value")]
        public decimal Price { get; set; }
    }
}