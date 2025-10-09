using Microsoft.AspNetCore.Mvc;
using SelfFinanceAPI.Mappers;
using SelfFinance.Shared.Dtos.OperationDtos;
using SelfFinance.Core.Services;
using SelfFinance.Core.Exceptions;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using SelfFinance.Core.Models;

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

        private int GetUserIdFromToken()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out var userId))
                throw new Exception("Invalid user ID in token");

            return userId;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOperations()
        {
            var userId = GetUserIdFromToken();
            var operations = await _operationService.GetAllOperationsAsync(userId);
            var operationsDtos = operations.Select(o => o.ToDisplayOperationDto());
            return Ok(operationsDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOperation(int id)
        {
            var userId = GetUserIdFromToken();
            var operation = await _operationService.GetOperationAsync(id, userId);

            return Ok(operation.ToDisplayOperationDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateOperation([FromBody] CreateOperationDto dto)
        {
            if (!ModelState.IsValid)
                throw new ValidationException("Invalid operation data.");
            var userId = GetUserIdFromToken();

            var operation = dto.ToOperationFromCreateOperationDto();

            operation.UserId = userId;

            var createdOperation = await _operationService.CreateOperationAsync(operation, userId);
            var operationDto = createdOperation.ToDisplayOperationDto();

            return CreatedAtAction(nameof(GetOperation), new { id = createdOperation.Id }, operationDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOperation(int id, [FromBody] UpdateOperationDto dto)
        {
            if (!ModelState.IsValid)
                throw new ValidationException("Invalid operation data.");

            var userId = GetUserIdFromToken();
            var operation = await _operationService.GetOperationAsync(id, userId);
            if (operation == null)
                throw new NotFoundException($"Operation with id {id} not found.");


            operation.IsIncome = dto.IsIncome;
            operation.Amount = dto.Amount;
            operation.CategoryId = dto.CategoryId;
            operation.Date = dto.Date;
            operation.Description = dto.Description;

            var updated = await _operationService.UpdateOperationAsync(operation, userId);

            return Ok(updated.ToDisplayOperationDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperation(int id)
        {
            var userId = GetUserIdFromToken();
            var operation = await _operationService.GetOperationAsync(id, userId);
            if (operation == null)
                throw new NotFoundException($"Operation with id {id} not found.");

            await _operationService.DeleteOperationAsync(operation);
            return NoContent();
        }
    }
}
