using ECommerceApp.Domain.Entities;
using ECommerceApp.PresentationLayer.Modules.Categories.ViewModel;

namespace ECommerceApp.PresentationLayer.Modules.Categories.Interface
{
    public interface ICategoryViewModelProvider
    {
        Task<CategoryEditViewModel?> GetByIdAsync(int id);
        Task<IReadOnlyList<CategoryListViewModel>> GetAllAsync();
        Task<Category> AddAsync(CategoryCreateViewModel categoryVm);
        Task UpdateAsync(CategoryEditViewModel categoryVm);
        Task<bool> DeleteAsync(int id);
    }
}
