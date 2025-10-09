using Microsoft.EntityFrameworkCore;
using SelfFinance.Core.Models;
using Microsoft.Extensions.Hosting;

namespace SelfFinance.Core.Data
{
    public class SelfFinanceAPIContext: DbContext
    {
        private readonly IHostEnvironment? _env;
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        public SelfFinanceAPIContext(DbContextOptions<SelfFinanceAPIContext> options)
        : base(options)
        {
        }

        public SelfFinanceAPIContext(DbContextOptions<SelfFinanceAPIContext> options, IHostEnvironment env)
            : base(options)
        {
            _env = env;
        }       
    }
}
