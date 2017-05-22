﻿using System;
using System.ComponentModel.DataAnnotations;

namespace TemplateProject.Sl.WebApi.Model
{
    public class Product
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [RegularExpression("[a-zA-Z]+")]
        public string Name { get; set; }

        [Required]
        [RegularExpression("[a-zA-Z]+")]
        public string Category { get; set; }
      
        [Required]
        [Range(0.5, double.PositiveInfinity)]
        public decimal Price { get; set; }
        
        [Required]
        public DateTimeOffset DateAdded { get; set; }
    }
}