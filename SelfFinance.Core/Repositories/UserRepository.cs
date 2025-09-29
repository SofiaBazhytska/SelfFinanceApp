using Microsoft.EntityFrameworkCore;
using SelfFinance.Core.Data;
using SelfFinance.Core.Models;

namespace SelfFinance.Core.Repositories
{
    public class UserRepository(SelfFinanceAPIContext context)
    {
        private readonly SelfFinanceAPIContext _context = context;

        public async Task<bool> UserExistAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.UserName == username);
        }

        public async Task<User?> GetUserAsync(string Username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == Username);
        }

        public async Task CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
