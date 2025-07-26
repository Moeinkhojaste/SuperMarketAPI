// Services/Interfaces/ICategoryService.cs

using SuperMarketAPI.DTOs;

namespace SuperMarketAPI.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryReadDto>> GetAllAsync();
    Task<CategoryReadDto?> GetByIdAsync(int id);
    Task<CategoryReadDto> CreateAsync(CategoryCreateDto dto);
    Task UpdateAsync(int id, CategoryUpdateDto dto);
    Task DeleteAsync(int id);
}
