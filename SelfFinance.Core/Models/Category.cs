using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfFinance.Core.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public bool IsIncome {  get; set; }
        public ICollection<Operation> Operations { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
