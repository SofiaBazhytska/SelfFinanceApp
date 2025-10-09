using Microsoft.EntityFrameworkCore;
using SelfFinance.Core.Data;
using SelfFinance.Core.Models;

namespace SelfFinance.Core.Repositories
{
    public class CategoryRepository
    {
        private readonly SelfFinanceAPIContext _context;
        public CategoryRepository(SelfFinanceAPIContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllCategoriesForUserAsync(int userId)
        {
            return await _context.Categories
                         .Where(c => c.UserId == userId)
                         .ToListAsync();
        }

        public async Task<Category?> GetCategoryAsync(int id, int userId)
        {
            return await _context.Categories
                .Include(c=>c.Operations)
                .FirstOrDefaultAsync(c => c.CategoryId == id && c.UserId == userId);
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<Category?> UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return category;
        }
    }
}
