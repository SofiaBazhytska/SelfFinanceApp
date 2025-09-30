using SelfFinance.Shared.Dtos.OperationDtos;

namespace SelfFinance.Shared.Dtos.ReportDtos
{
    public class DisplayPeriodReportDto
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }

        public ICollection<DisplayOperationDto> Operations { get; set; }
    }
}
