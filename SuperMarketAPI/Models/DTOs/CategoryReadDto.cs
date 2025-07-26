// DTOs/CategoryReadDto.cs

using SuperMarketAPI.Models.DTOs;

namespace SuperMarketAPI.DTOs;

public class CategoryReadDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<CategoryProductDto> Products { get; set; } = new();
}
