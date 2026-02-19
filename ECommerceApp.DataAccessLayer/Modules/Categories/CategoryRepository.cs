using ECommerceApp.DataAccessLayer.Data;
using ECommerceApp.DataAccessLayer.Modules.Categories.Interfaces;
using ECommerceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.DataAccessLayer.Modules.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ECommerceDbContext _context;
        public CategoryRepository(ECommerceDbContext context)
        {
            _context = context;
        }
        public async Task<Category> AddAsync(Category category)
        {
            _context.Catagories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteAsync(Category category)
        {
            _context.Catagories.Remove(category);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IReadOnlyList<Category>> GetAllAsync()
        {
            return await _context.Catagories
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Catagories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Catagories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByNameAsync(string name, int? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            var query = _context.Catagories.
                AsNoTracking()
                .Where(c => c.Name.ToLower().Trim() == name.ToLower().Trim());

            if (excludeId.HasValue)
            {
                query = query.Where(c => c.Id != excludeId.Value);
            }
            return await query.AnyAsync();
        }
    }
}
