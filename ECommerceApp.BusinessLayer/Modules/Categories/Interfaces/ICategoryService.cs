using ECommerceApp.Domain.Entities;

namespace ECommerceApp.BusinessLayer.Modules.Categories.Interface
{
    public interface ICategoryService
    {
        Task<Category?> GetByIdAsync(int id);
        Task<IReadOnlyList<Category>> GetAllAsync();
        Task<Category> AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task<bool> DeleteAsync(Category category);
    }
}
