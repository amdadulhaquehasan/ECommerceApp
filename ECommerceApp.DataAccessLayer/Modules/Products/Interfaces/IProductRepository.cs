using ECommerceApp.Domain.Entities;

namespace ECommerceApp.DataAccessLayer.Modules.Products.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetAllAsync();
        Task<Product> AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task<bool> DeleteAsync(Product product);
        Task<bool> ExistsBySkuAsync(string sku, int? excludedId = null);
    }
}
