using ECommerceApp.BusinessLayer.Exceptions;
using ECommerceApp.BusinessLayer.Modules.Products.Interfaces;
using ECommerceApp.DataAccessLayer.Modules.Products.Interfaces;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.BusinessLayer.Modules.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> AddAsync(Product product)
        {
            product.CreatedDate = DateTime.UtcNow;
            product.UpdatedDate = DateTime.UtcNow;
            bool exists = await _productRepository.ExistsBySkuAsync(product.SKU);
            if (exists)
            {
                throw new InvalidUserInputException("A product with the same SKU already exists.");
            }

            return await _productRepository.AddAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            product.UpdatedDate = DateTime.UtcNow;
            bool exists = await _productRepository.ExistsBySkuAsync(product.SKU, product.Id);
            if (exists)
            {
                throw new InvalidUserInputException("A product with the same SKU already exists.");
            }
            await _productRepository.UpdateAsync(product);
        }

        public async Task<bool> DeleteAsync(Product product)
        {
            return await _productRepository.DeleteAsync(product);
        }
    }
}
