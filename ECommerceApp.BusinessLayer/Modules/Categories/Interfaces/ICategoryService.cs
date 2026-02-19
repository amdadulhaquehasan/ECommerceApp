using ECommerceApp.Domain.Entities;

namespace ECommerceApp.BusinessLayer.Module.Categories.Interface
{
    public interface ICategoryService
    {
        Task<bool> CreateCategoryAsync(Category category);
    }
}
