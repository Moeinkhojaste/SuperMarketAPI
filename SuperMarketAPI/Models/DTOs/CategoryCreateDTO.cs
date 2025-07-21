using System.ComponentModel.DataAnnotations;

namespace SuperMarketAPI.Models.DTOs
{
    public class CategoryCreateDTO
    {
        //[Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
    }
}
