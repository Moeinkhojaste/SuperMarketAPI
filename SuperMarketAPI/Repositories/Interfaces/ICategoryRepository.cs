// Repositories/Interfaces/ICategoryRepository.cs

using SuperMarketAPI.Models;

namespace SuperMarketAPI.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    Task AddAsync(Category category);
    Task UpdateAsync(Category category);
    Task DeleteAsync(Category category);
}
