using SelfFinance.Shared.Dtos.OperationDtos;

namespace SelfFinance.Shared.Dtos.ReportDtos
{
    public class DisplayDailyReportDto
    {
        public DateOnly Date { get; set; }

        public decimal TotalIncome { get; set; }

        public decimal TotalExpenses { get; set; }

        public List<DisplayOperationDto> Operations { get; set; }
    }
}
