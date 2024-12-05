using System.ComponentModel.DataAnnotations;
namespace Workshop.DTOs
{
    /// <summary>
    /// Data transfer object (DTO) używane do reprezentowania danych logowania użytkownika.
    /// </summary>
    public class LoginUserDto
    {
        /// <summary>
        /// Adres e-mail użytkownika.
        /// Pole jest obowiązkowe i musi zawierać prawidłowy format adresu e-mail.
        /// </summary>
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public required string Email { get; set; }

        /// <summary>
        /// Hasło użytkownika.
        /// Pole jest obowiązkowe i powinno zawierać hasło do logowania.
        /// </summary>
        [Required(ErrorMessage = "Password is required.")] 
        public required string Password { get; set; }
    }
}
