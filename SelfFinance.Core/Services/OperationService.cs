using Microsoft.EntityFrameworkCore;
using SelfFinance.Core.Exceptions;
using SelfFinance.Core.Models;
using SelfFinance.Core.Repositories;
using System.ComponentModel.DataAnnotations;
using Operation = SelfFinance.Core.Models.Operation;

namespace SelfFinance.Core.Services
{
    public class OperationService
    {
        private readonly OperationRepository _operation;
        private readonly CategoryRepository _category;

        public OperationService(OperationRepository operation, CategoryRepository category)
        {
            _operation = operation;
            _category = category;
        }

        public async Task<IEnumerable<Operation>> GetAllOperationsAsync()
        {
            return await _operation.GetAllOperationsAsync();
        }

        public async Task<Operation> GetOperationAsync(int id)
        {
            var operation = await _operation.GetOperationAsync(id);
            if (operation == null)
                throw new NotFoundException($"Operation with id {id} not found.");

            return operation;
        }

        public async Task<Operation> CreateOperationAsync(Operation operation)
        {
            var category = await _category.GetCategoryAsync(operation.CategoryId);
            if (category == null)
                throw new ValidationException($"Category with id {operation.CategoryId} does not exist.");

            return await _operation.CreateOperationAsync(operation);
        }

        public async Task<Operation?> UpdateOperationAsync(Operation operation)
        {
            var category = await _category.GetCategoryAsync(operation.CategoryId);
            if (category == null)
                throw new ValidationException($"Category with id {operation.CategoryId} does not exist.");

            return await _operation.UpdateOperationAsync(operation);
        }

        public async Task DeleteOperationAsync(Operation operation)
        {
            await _operation.DeleteOperationAsync(operation);
        }
    }

}
