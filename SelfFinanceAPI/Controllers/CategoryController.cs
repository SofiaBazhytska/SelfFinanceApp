using Microsoft.AspNetCore.Mvc;
using SelfFinance.Core.Models;
using SelfFinance.Shared.Dtos.CategoryDtos;
using SelfFinanceAPI.Mappers;
using SelfFinance.Core.Services;
using SelfFinance.Core.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace SelfFinanceAPI.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            var categoryDtos = categories.Select(c => c.ToDisplayCategoryDto());
            return Ok(categoryDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryAsync(id)
                ?? throw new NotFoundException($"Category with id {id} not found.");

            return Ok(category.ToDisplayCategoryDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto dto)
        {
            if (!ModelState.IsValid)
                throw new ValidationException("Invalid category data.");

            var categoryModel = dto.ToCategoryFromCreateCategoryDto();
            var createdCategory = await _categoryService.CreateCategoryAsync(categoryModel);
            var categoryDto = createdCategory.ToDisplayCategoryDto();

            return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.CategoryId }, categoryDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDto dto)
        {
            if (!ModelState.IsValid)
                throw new ValidationException("Invalid update data.");

            var category = await _categoryService.GetCategoryAsync(id)
                ?? throw new NotFoundException($"Category with id {id} not found.");

            category.Name = dto.Name;
            category.IsIncome = dto.IsIncome;

            var updated = await _categoryService.UpdateCategoryAsync(category);

            return Ok(updated.ToDisplayCategoryDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryService.GetCategoryAsync(id)
                ?? throw new NotFoundException($"Category with id {id} not found.");

            await _categoryService.DeleteCategoryAsync(category);
            return NoContent();
        }
    }
}
