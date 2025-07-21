using System.ComponentModel.DataAnnotations;

namespace SuperMarketAPI.Models.DTOs
{
    public class ProductCreateDto
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public double Price { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Stock must be greater than or equal to zero.")]
        public int Stock { get; set; }
        [Required(ErrorMessage = "You must assign a category to this product.")]
        public int CategoryId { get; set; } // Foreign key for Category
    }
}
