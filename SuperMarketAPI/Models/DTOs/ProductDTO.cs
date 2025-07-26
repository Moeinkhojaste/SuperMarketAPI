namespace SuperMarketAPI.Models.DTOs;

public class ProductDto
{
    public string? Name { get; set; }
    public int Price { get; set; }
}

public class ProductCreateDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
}

public class ProductUpdateDto : ProductCreateDto
{
    public int Id { get; set; }
}

public class ProductReadDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string CategoryName { get; set; } = string.Empty;
}
