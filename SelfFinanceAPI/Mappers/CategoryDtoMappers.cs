using SelfFinance.Core.Models;
using SelfFinance.Shared.Dtos.CategoryDtos;

namespace SelfFinanceAPI.Mappers
{
    public static class CategoryDtoMappers
    {
        public static DisplayCategoryDto ToDisplayCategoryDto(this Category model)
        {
            return new DisplayCategoryDto
            {
                Id = model.CategoryId,
                Name = model.Name,
                TypeName  = model.IsIncome ? "Income" : "Expenses",
                IsIncome = model.IsIncome
            };
        }

        public static Category ToCategoryFromCreateCategoryDto(this CreateCategoryDto dto) 
        {
            return new Category
            {
                Name = dto.Name,
                IsIncome = dto.IsIncome
            };
        }
    }
}
