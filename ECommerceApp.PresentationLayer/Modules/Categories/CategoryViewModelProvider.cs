using AutoMapper;
using ECommerceApp.BusinessLayer.Modules.Categories.Interface;
using ECommerceApp.Domain.Entities;
using ECommerceApp.PresentationLayer.Modules.Categories.Interface;
using ECommerceApp.PresentationLayer.Modules.Categories.ViewModel;

namespace ECommerceApp.PresentationLayer.Modules.Categories
{
    public class CategoryViewModelProvider : ICategoryViewModelProvider
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryViewModelProvider(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CategoryListViewModel>> GetAllAsync()
        {
            var categoryList = await _categoryService.GetAllAsync();
            return _mapper.Map<IReadOnlyList<CategoryListViewModel>>(categoryList);
        }

        public async Task<CategoryEditViewModel?> GetByIdAsync(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return null;

            return _mapper.Map<CategoryEditViewModel>(category);
        }

        public async Task<Category> AddAsync(CategoryCreateViewModel categoryVm)
        {
            Category category = _mapper.Map<Category>(categoryVm);
            await _categoryService.AddAsync(category);
            return category;
        }

        public async Task UpdateAsync(CategoryEditViewModel categoryVm)
        {
            Category category = _mapper.Map<Category>(categoryVm);
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
