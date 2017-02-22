using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Product
    {
        [Required(ErrorMessage = "Product must have an Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product must have a Name")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Product must have a Category")]
        [Required]
        [StringLength(10)]
        public string Category { get; set; }
      
        [Required(ErrorMessage = "Product must have a Price")]
        public decimal Price { get; set; }
    }
}