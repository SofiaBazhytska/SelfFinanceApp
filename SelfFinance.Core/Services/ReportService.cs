using SelfFinance.Core.Models;
using SelfFinance.Core.Repositories;

namespace SelfFinance.Core.Services
{
    public class ReportService
    {
        private readonly OperationRepository _operationRepository;

        public ReportService(OperationRepository operationRepository)
        {
            _operationRepository = operationRepository;
        }

        public async Task<DailyReport> GetDailyReportAsync(DateOnly date)
        {
            var operations = await _operationRepository.GetOperationsByDateAsync(date);

            return new DailyReport
            {
                Date = date,
                Operations = operations.ToList(),
                TotalIncome = operations.Where(o => o.IsIncome).Sum(o => o.Amount),
                TotalExpenses = operations.Where(o => !o.IsIncome).Sum(o => o.Amount)
            };
        }

        public async Task<PeriodReport> GetPeriodReportAsync(DateOnly startDate, DateOnly endDate)
        {
            var operations = await _operationRepository.GetOperationsByPeriodAsync(startDate, endDate);

            return new PeriodReport
            {
                StartDate = startDate,
                EndDate = endDate,
                Operations = operations.ToList(),
                TotalIncome = operations.Where(o => o.IsIncome).Sum(o => o.Amount),
                TotalExpenses = operations.Where(o => !o.IsIncome).Sum(o => o.Amount)
            };
        }
    }
}