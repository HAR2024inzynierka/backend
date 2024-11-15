using System.ComponentModel.DataAnnotations;

namespace Workshop.DTOs
{
    public class UpdateUserDto
    {
        [Required(ErrorMessage = "Login is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Login must be between 3 and 50 characters.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        [Phone(ErrorMessage = "Invalid phone format")]
        public string PhoneNumber { get; set; }
    }
}
