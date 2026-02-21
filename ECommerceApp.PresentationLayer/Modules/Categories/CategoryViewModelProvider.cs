using ECommerceApp.BusinessLayer.Modules.Categories.Interface;
using ECommerceApp.Domain.Entities;
using ECommerceApp.PresentationLayer.Modules.Categories.Interface;
using ECommerceApp.PresentationLayer.Modules.Categories.ViewModel;

namespace ECommerceApp.PresentationLayer.Modules.Categories
{
    public class CategoryViewModelProvider : ICategoryViewModelProvider
    {
        private readonly ICategoryService _categoryService;
        public CategoryViewModelProvider(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IReadOnlyList<CategoryListViewModel>> GetAllAsync()
        {
            var categoryList = await _categoryService.GetAllAsync();
            return categoryList.Select(c => new CategoryListViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList();
        }

        public async Task<CategoryEditViewModel?> GetByIdAsync(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return null;

            return new CategoryEditViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task<Category> AddAsync(CategoryCreateViewModel categoryVm)
        {
            Category category = new Category();
            category.Name = categoryVm.Name;
            category.Description = categoryVm.Description;
            await _categoryService.AddAsync(category);
            return category;
        }

        public async Task UpdateAsync(CategoryEditViewModel categoryVm)
        {
            Category category = new Category();
            category.Id = categoryVm.Id;
            category.Name = categoryVm.Name;
            category.Description = categoryVm.Description;
            category.CreatedDate = categoryVm.CreatedDate;

            await _categoryService.UpdateAsync(category);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return false;

            return await _categoryService.DeleteAsync(category);
        }
    }
}
