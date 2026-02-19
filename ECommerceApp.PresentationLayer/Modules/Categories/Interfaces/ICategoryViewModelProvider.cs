using ECommerceApp.PresentationLayer.Module.Categories.ViewModel;

namespace ECommerceApp.PresentationLayer.Module.Categories.Interface
{
    public interface ICategoryViewModelProvider
    {
        Task<(bool Success, string? ErrorMessage)> CreateCategoryAsync(CategoryCreateViewModel categoryCreateViewModel);
    }
}
