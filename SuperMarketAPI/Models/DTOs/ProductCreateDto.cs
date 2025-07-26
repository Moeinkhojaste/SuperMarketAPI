using System.ComponentModel.DataAnnotations;

namespace SuperMarketAPI.DTOs;

public class ProductCreateDto
{
    //[Required]
    public string Name { get; set; } = string.Empty;

    //[Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    //[Range(0, int.MaxValue)]
    public int Stock { get; set; }

    //[Required]
    public int CategoryId { get; set; }
}