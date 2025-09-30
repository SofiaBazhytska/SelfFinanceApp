using Microsoft.AspNetCore.Mvc;
using SelfFinanceAPI.Mappers;
using SelfFinance.Shared.Dtos.OperationDtos;
using SelfFinance.Core.Services;
using SelfFinance.Core.Exceptions;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace SelfFinanceAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/operations")]
    public class OperationController : Controller
    {
        private readonly OperationService _operationService;

        public OperationController(OperationService operationService)
        {
            _operationService = operationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOperations()
        {
            var operations = await _operationService.GetAllOperationsAsync();
            var operationsDtos = operations.Select(o => o.ToDisplayOperationDto());
            return Ok(operationsDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOperation(int id)
        {
            var operation = await _operationService.GetOperationAsync(id);

            return Ok(operation.ToDisplayOperationDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateOperation([FromBody] CreateOperationDto dto)
        {
            if (!ModelState.IsValid)
                throw new ValidationException("Invalid operation data.");

            var operation = dto.ToOperationFromCreateOperationDto();
            var createdOperation = await _operationService.CreateOperationAsync(operation);
            var operationDto = createdOperation.ToDisplayOperationDto();

            return CreatedAtAction(nameof(GetOperation), new { id = createdOperation.Id }, operationDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOperation(int id, [FromBody] UpdateOperationDto dto)
        {
            if (!ModelState.IsValid)
                throw new ValidationException("Invalid operation data.");

            var operation = await _operationService.GetOperationAsync(id);
            if (operation == null)
                throw new NotFoundException($"Operation with id {id} not found.");

            operation.IsIncome = dto.IsIncome;
            operation.Amount = dto.Amount;
            operation.CategoryId = dto.CategoryId;
            operation.Date = dto.Date;
            operation.Description = dto.Description;

            var updated = await _operationService.UpdateOperationAsync(operation);

            return Ok(updated.ToDisplayOperationDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperation(int id)
        {
            var operation = await _operationService.GetOperationAsync(id);
            if (operation == null)
                throw new NotFoundException($"Operation with id {id} not found.");

            await _operationService.DeleteOperationAsync(operation);
            return NoContent();
        }
    }
}
