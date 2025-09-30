using SelfFinance.Core.Models;
using SelfFinance.Core.Repositories;
using System.ComponentModel.DataAnnotations;

namespace SelfFinance.Core.Services
{
    public class CategoryService
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryService(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllCategoriesAsync();
        }

        public async Task<Category?> GetCategoryAsync(int id)
        {
            return await _categoryRepository.GetCategoryAsync(id);
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            return await _categoryRepository.CreateCategoryAsync(category);
        }

        public async Task<Category?> UpdateCategoryAsync(Category category)
        {
            return await _categoryRepository.UpdateCategoryAsync(category);
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            if (category.Operations != null && category.Operations.Any())
                throw new ValidationException("Cannot delete the category because it contains operations.");

            await _categoryRepository.DeleteCategoryAsync(category);
        }
    }
}
