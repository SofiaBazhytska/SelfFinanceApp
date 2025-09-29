using System.ComponentModel.DataAnnotations;

namespace SelfFinance.Shared.Dtos.UserDtos
{
    public class UserDto
    {
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Пароль має бути мінімум 6 символів")]
        public string Password { get; set; } = string.Empty;
    }
}
