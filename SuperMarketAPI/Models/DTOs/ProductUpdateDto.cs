// DTOs/ProductUpdateDto.cs

namespace SuperMarketAPI.DTOs;

public class ProductUpdateDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int CategoryId { get; set; }
}
