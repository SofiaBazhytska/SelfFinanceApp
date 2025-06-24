namespace SelfFinance.Core.Models
{
    public class DailyReport
    {
        public DateOnly Date { get; set; }
        public List<Operation> Operations { get; set; } = new();
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
    }
}
