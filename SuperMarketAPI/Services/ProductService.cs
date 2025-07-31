// Services/ProductService.cs

using AutoMapper;
using SuperMarketAPI.DTOs;
using SuperMarketAPI.Models;
using SuperMarketAPI.Repositories.Interfaces;
using SuperMarketAPI.Services.Interfaces;

namespace SuperMarketAPI.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<ProductReadDto> CreateAsync(ProductCreateDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Product name is required");

        var product = _mapper.Map<Product>(dto);
        await _repo.AddAsync(product);

        return _mapper.Map<ProductReadDto>(product);
    }

    public async Task<List<ProductReadDto>> GetAllAsync()
    {
        var products = await _repo.GetAllAsync();
        return _mapper.Map<List<ProductReadDto>>(products);
    }

    public async Task<ProductReadDto?> GetByIdAsync(int id)
    {
        var product = await _repo.GetByIdAsync(id);
        return _mapper.Map<ProductReadDto>(product);
    }

    public async Task<bool> UpdateAsync(int id, ProductUpdateDto dto)
    {
        var product = await _repo.GetByIdAsync(id);
        if (product == null)
            return false;

        _mapper.Map(dto, product);
        await _repo.UpdateAsync(product);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _repo.GetByIdAsync(id);
        if (product == null)
            return false;

        await _repo.DeleteAsync(product);
        return true;
    }
}
