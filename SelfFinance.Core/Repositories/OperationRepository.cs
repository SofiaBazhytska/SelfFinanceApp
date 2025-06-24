using Microsoft.EntityFrameworkCore;
using SelfFinance.Core.Data;
using SelfFinance.Core.Models;

namespace SelfFinance.Core.Repositories
{
    public class OperationRepository
    {
        private readonly SelfFinanceAPIContext _context;
        public OperationRepository(SelfFinanceAPIContext context)
        {
            _context = context;
        }

        public async Task<List<Operation>> GetAllOperationsAsync()
        {
            return await _context.Operations
                .Include(c=>c.Category)
                .ToListAsync();
        }

        public async Task<Operation?> GetOperationAsync(int id)
        {
            return await _context.Operations
                .Include(o => o.Category)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<List<Operation>> GetOperationsByDateAsync(DateOnly date)
        {
            return await _context.Operations
                .Include(o => o.Category)
                .Where(o => o.Date == date)
                .ToListAsync();
        }

        public async Task<List<Operation>> GetOperationsByPeriodAsync(DateOnly startDate, DateOnly endDate)
        {
            return await _context.Operations
                .Include(o => o.Category)
                .Where(o => o.Date >= startDate && o.Date <= endDate)
                .ToListAsync();
        }

        public async Task<Operation> CreateOperationAsync(Operation operation)
        {
            _context.Operations.Add(operation);
            await _context.SaveChangesAsync();

            return await GetOperationAsync(operation.Id);
        }

        public async Task<Operation?> UpdateOperationAsync(Operation operation)
        {
            _context.Operations.Update(operation);
            await _context.SaveChangesAsync();
            await _context.Entry(operation).Reference(o => o.Category).LoadAsync();

            return operation;
        }

        public async Task DeleteOperationAsync(Operation operation)
        {
            _context.Operations.Remove(operation);
            await _context.SaveChangesAsync();
        }
    }
}
