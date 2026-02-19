using ECommerceApp.PresentationLayer.Module.Categories.Interface;
using ECommerceApp.PresentationLayer.Module.Categories.ViewModel;

namespace ECommerceApp.PresentationLayer.Module.Categories
{
    public class CategoryViewModelProvider : ICategoryViewModelProvider
    {
        public Task<(bool Success, string? ErrorMessage)> CreateCategoryAsync(CategoryCreateViewModel categoryCreateViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
