using ECommerceApp.BusinessLayer.Module.Categories.Interface;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.BusinessLayer.Module.Categories
{
    public class CategoryService : ICategoryService
    {
        public Task<bool> CreateCategoryAsync(Category category)
        {
            throw new NotImplementedException();
            //category.CreatedDate = DateTime.Now;
            //Category category1 = _context.Categories.FirstOrDefault(m => m.Name == category.Name);
        }
    }
}
