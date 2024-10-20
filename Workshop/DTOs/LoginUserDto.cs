using System.ComponentModel.DataAnnotations;
namespace Workshop.DTOs
{
    public class LoginUserDto
    {
        [Required]public string Email { get; set; }
        [Required] public string Password { get; set; }
    }
}
