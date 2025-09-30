namespace SelfFinance.Shared.Dtos.OperationDtos
{
    public class DisplayOperationDto
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
    }
}
