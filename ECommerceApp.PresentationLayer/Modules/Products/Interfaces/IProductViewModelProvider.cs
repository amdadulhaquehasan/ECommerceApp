using ECommerceApp.Domain.Entities;
using ECommerceApp.PresentationLayer.Modules.Products.ViewModel;

namespace ECommerceApp.PresentationLayer.Modules.Products.Interfaces
{
    public interface IProductViewModelProvider
    {
        Task<ProductEditViewModel?> GetByIdAsync(int id);
        Task<IReadOnlyList<ProductListViewModel>> GetAllAsync();
        Task<Product> AddAsync(ProductCreateViewModel productVm);
        Task UpdateAsync(ProductEditViewModel productVm);
        Task<bool> DeleteAsync(int id);
    }
}
