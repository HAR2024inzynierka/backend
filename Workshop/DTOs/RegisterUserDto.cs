using System.ComponentModel.DataAnnotations;

namespace Workshop.DTOs
{
    public class RegisterUserDto
    {
        [Required] public string Login { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }
    }
}
