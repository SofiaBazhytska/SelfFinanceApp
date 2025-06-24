using System.ComponentModel.DataAnnotations;

namespace SelfFinance.Shared.Dtos.CategoryDtos
{
    public class UpdateCategoryDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required]
        public bool IsIncome { get; set; }
    }
}
