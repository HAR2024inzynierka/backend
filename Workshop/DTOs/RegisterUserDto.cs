using System.ComponentModel.DataAnnotations;

namespace Workshop.DTOs
{
    /// <summary>
    /// Data transfer object (DTO) używane do reprezentowania danych rejestracji użytkownika.
    /// </summary>
    public class RegisterUserDto
    {
        /// <summary>
        /// Login użytkownika.
        /// Pole jest obowiązkowe i musi zawierać od 3 do 50 znaków.
        /// </summary>
        [Required(ErrorMessage = "Login is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Login must be between 3 and 50 characters.")]
        public required string Login { get; set; }

        /// <summary>
        /// Adres e-mail użytkownika.
        /// Pole jest obowiązkowe i musi zawierać prawidłowy format adresu e-mail.
        /// </summary>
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public required string Email { get; set; }

        /// <summary>
        /// Hasło użytkownika.
        /// Pole jest obowiązkowe i musi zawierać co najmniej 6 znaków.
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public required string Password { get; set; }
    }
}
