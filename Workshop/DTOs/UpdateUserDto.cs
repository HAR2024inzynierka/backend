using System.ComponentModel.DataAnnotations;

namespace Workshop.DTOs
{
    /// <summary>
    /// Data transfer object (DTO) używane do reprezentowania danych aktualizacji użytkownika.
    /// </summary>
    public class UpdateUserDto
    {
        /// <summary>
        /// Login użytkownika, który ma być zaktualizowany.
        /// Pole jest obowiązkowe i musi zawierać od 3 do 50 znaków.
        /// </summary>
        [Required(ErrorMessage = "Login is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Login must be between 3 and 50 characters.")]
        public required string Login { get; set; }

        /// <summary>
        /// Adres e-mail użytkownika, który ma być zaktualizowany.
        /// Pole jest obowiązkowe i musi zawierać prawidłowy format adresu e-mail.
        /// </summary>
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public required string Email { get; set; }

        /// <summary>
        /// Numer telefonu użytkownika, który ma być zaktualizowany.
        /// Pole jest obowiązkowe i musi zawierać prawidłowy format numeru telefonu.
        /// </summary>
        [Phone(ErrorMessage = "Invalid phone format")]
        public string? PhoneNumber { get; set; }
    }
}
