using SelfFinance.Shared.Dtos.OperationDtos;
using SelfFinance.Core.Models;

namespace SelfFinanceAPI.Mappers
{
    public static class OperationDtoMappers
    {
        public static DisplayOperationDto ToDisplayOperationDto(this Operation model)
        {
            return new DisplayOperationDto
            {
                Id = model.Id,
                Date = model.Date,
                Amount = model.Amount,
                CategoryId = model.CategoryId,
                CategoryName = model.Category.Name,
                TypeName = model.IsIncome ? "Income" : "Expenses",
                Description = model.Description
            };
        }

        public static Operation ToOperationFromCreateOperationDto(this CreateOperationDto model)
        {
            return new Operation
            {
                IsIncome = model.IsIncome,
                Amount = model.Amount,
                CategoryId = model.CategoryId,
                Date = model.Date,
                Description = model.Description,
            };
        }
    }
}
