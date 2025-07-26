using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SuperMarketAPI.Data;
using SuperMarketAPI.Models;
using SuperMarketAPI.Models.DTOs;
using SuperMarketAPI.Services.Interfaces;

namespace SuperMarketAPI.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ProductService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ProductReadDto>> GetAllAsync()
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .ToListAsync();
        await _context.SaveChangesAsync();
        return _mapper.Map<List<ProductReadDto>>(products);
        
    }

    public async Task<ProductReadDto?> GetByIdAsync(int id)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);

        return product == null ? null : _mapper.Map<ProductReadDto>(product);
    }

    public async Task<ProductReadDto> CreateAsync(ProductCreateDto dto)
    {
        // AutoMapper will handle this conversion
        var product = _mapper.Map<Product>(dto);

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        // Explicitly load the category for the response
        await _context.Entry(product)
            .Reference(p => p.Category)
            .LoadAsync();

        // Map to read DTO
        return _mapper.Map<ProductReadDto>(product);
    }

    public async Task<bool> UpdateAsync(int id, ProductUpdateDto dto)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;

        _mapper.Map(dto, product);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
}

// new