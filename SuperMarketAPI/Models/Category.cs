using System.ComponentModel.DataAnnotations;

namespace SuperMarketAPI.Models
{
    public class Category
    {
        public int Id { get; set; }

        //[Required]
        public string Name { get; set; }

        // Navigation
        public List<Product> Products { get; set; }
    }
}
