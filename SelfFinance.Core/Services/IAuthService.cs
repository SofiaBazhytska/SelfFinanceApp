using SelfFinance.Core.Models;
using SelfFinance.Shared.Dtos.UserDtos;

namespace SelfFinance.Core.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDto request);
        Task<string?> LoginAsync(UserDto request);
    }
}
