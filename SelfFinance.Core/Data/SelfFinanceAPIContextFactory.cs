using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfFinance.Core.Data
{
    public class SelfFinanceAPIContextFactory : IDesignTimeDbContextFactory<SelfFinanceAPIContext>
    {
        public SelfFinanceAPIContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<SelfFinanceAPIContext>();

            var connectionString = config.GetConnectionString("DefaultConnection")
                ?? Environment.GetEnvironmentVariable("DB_CONNECTION_SELF_FINANCE_API");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Connection string not found.");
            }

            optionsBuilder.UseSqlServer(connectionString);

            return new SelfFinanceAPIContext(optionsBuilder.Options);
        }
    }
}
