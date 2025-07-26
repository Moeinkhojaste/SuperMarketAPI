using SuperMarketAPI.Models.DTOs;

namespace SuperMarketAPI.DTOs;
public class CategoryDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<ProductDto>? Products { get; set; }
}
