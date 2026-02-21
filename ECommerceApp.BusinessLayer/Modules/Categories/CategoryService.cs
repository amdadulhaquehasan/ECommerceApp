using ECommerceApp.BusinessLayer.Exceptions;
using ECommerceApp.BusinessLayer.Modules.Categories.Interface;
using ECommerceApp.DataAccessLayer.Modules.Categories.Interfaces;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.BusinessLayer.Modules.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IReadOnlyList<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<Category> AddAsync(Category category)
        {
            category.CreatedDate = DateTime.UtcNow;
            bool exists = await _categoryRepository.ExistsByNameAsync(category.Name);
            if (exists)
            {
                throw new InvalidUserInputException($"A category with the name '{category.Name}' already exists.");
            }
            return await _categoryRepository.AddAsync(category);
        }

        public async Task UpdateAsync(Category category)
        {
            await _categoryRepository.UpdateAsync(category);
        }

        public async Task<bool> DeleteAsync(Category category)
        {
            return await _categoryRepository.DeleteAsync(category);
        }
    }
}
