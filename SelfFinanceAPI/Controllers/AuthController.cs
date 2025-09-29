using Microsoft.AspNetCore.Mvc;
using SelfFinance.Shared.Dtos.UserDtos;
using SelfFinance.Core.Models;
using SelfFinance.Core.Services;
using Microsoft.AspNetCore.Authorization;

namespace SelfFinanceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            var user = await authService.RegisterAsync(request);
            if(user is null)
            {
                return BadRequest("Username already exist");
            }

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody]UserDto request)
        {
            var token = await authService.LoginAsync(request);
            if(token is null)
            {
                return BadRequest("Invalid username or password");
            }

            return Ok(new LoginResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(1)
            });
        }

        [Authorize]
        [HttpGet]
        public IActionResult AuthenticatedOnlyEndpoint()
        {
            return Ok("You are authenticated");
        }
    }
}
