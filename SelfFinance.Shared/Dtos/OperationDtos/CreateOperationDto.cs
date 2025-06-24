using System.ComponentModel.DataAnnotations;

namespace SelfFinance.Shared.Dtos.OperationDtos
{
    public class CreateOperationDto
    {
        [Required]
        public bool IsIncome { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "CategoryId is required.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateOnly Date { get; set; }

        [MaxLength(250, ErrorMessage = "Description cannot exceed 250 characters.")]
        public string? Description { get; set; }
    }
}
