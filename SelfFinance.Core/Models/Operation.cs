using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfFinance.Core.Models
{
    public class Operation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool IsIncome { get; set; }
        [Column (TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public DateOnly Date {  get; set; }
        public string? Description { get; set; }
    }
}
