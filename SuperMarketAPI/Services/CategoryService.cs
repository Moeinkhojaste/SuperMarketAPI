// Services/CategoryService.cs

using AutoMapper;
using SuperMarketAPI.DTOs;
using SuperMarketAPI.Models;
using SuperMarketAPI.Repositories.Interfaces;
using SuperMarketAPI.Services.Interfaces;

namespace SuperMarketAPI.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repo;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryReadDto>> GetAllAsync()
    {
        var categories = await _repo.GetAllAsync();
        return _mapper.Map<IEnumerable<CategoryReadDto>>(categories);
    }

    public async Task<CategoryReadDto?> GetByIdAsync(int id)
    {
        var category = await _repo.GetByIdAsync(id);
        return category == null ? null : _mapper.Map<CategoryReadDto>(category);
    }

    public async Task<CategoryReadDto> CreateAsync(CategoryCreateDto dto)
    {
        var category = _mapper.Map<Category>(dto);
        await _repo.AddAsync(category);
        return _mapper.Map<CategoryReadDto>(category);
    }

    public async Task UpdateAsync(int id, CategoryUpdateDto dto)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing is null) throw new Exception("Category not found");

        _mapper.Map(dto, existing);
        await _repo.UpdateAsync(existing);
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _repo.GetByIdAsync(id);
        if (category is null) throw new Exception("Category not found");

        await _repo.DeleteAsync(category);
    }
}
