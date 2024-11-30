using System.ComponentModel.DataAnnotations;
namespace Workshop.DTOs
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")] 
        public required string Password { get; set; }
    }
}
