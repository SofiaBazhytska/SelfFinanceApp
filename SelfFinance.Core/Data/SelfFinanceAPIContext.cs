using Microsoft.EntityFrameworkCore;
using SelfFinance.Core.Models;
using Microsoft.Extensions.Hosting;

namespace SelfFinance.Core.Data
{
    public class SelfFinanceAPIContext: DbContext
    {
        private readonly IHostEnvironment _env;
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        public SelfFinanceAPIContext(DbContextOptions<SelfFinanceAPIContext> options, IHostEnvironment env) : base(options) 
        {
            _env = env;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (!_env.IsEnvironment("Testing"))
            {
                modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Salary", IsIncome = true },
                new Category { CategoryId = 2, Name = "Food", IsIncome = false }
                );

                modelBuilder.Entity<Operation>().HasData(
                    new Operation
                    {
                        Id = 1,
                        IsIncome = true,
                        Amount = 10000.00m,
                        CategoryId = 1,
                        Date = new DateOnly(2024, 5, 1),
                        Description = "Salary"
                    },
                    new Operation
                    {
                        Id = 2,
                        IsIncome = false,
                        Amount = 250.50m,
                        CategoryId = 2,
                        Date = new DateOnly(2024, 5, 2),
                        Description = "Food"
                    },
                    new Operation
                    {
                        Id = 3,
                        IsIncome = false,
                        Amount = 100.00m,
                        CategoryId = 2,
                        Date = new DateOnly(2024, 5, 3),
                        Description = "Food"
                    }
                );
            }
        }
    }
}
