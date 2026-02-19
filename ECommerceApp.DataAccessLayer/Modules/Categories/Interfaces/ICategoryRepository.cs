using ECommerceApp.Domain.Entities;

namespace ECommerceApp.DataAccessLayer.Modules.Categories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(int id);
        Task<IReadOnlyList<Category>> GetAllAsync();
        Task<Category> AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task<bool> DeleteAsync(Category category);
        Task<bool> ExistsByNameAsync(string name, int? excludeId = null);
    }
}
