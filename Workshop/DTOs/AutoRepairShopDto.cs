using System.ComponentModel.DataAnnotations;

namespace Workshop.DTOs
{
    /// <summary>
    /// Data transfer object (DTO) używane do reprezentowania danych warsztatu w systemie.
    /// </summary>
    public class AutoRepairShopDto
	{
        /// <summary>
        /// Adres e-mail warsztatu.
        /// Pole jest obowiązkowe i musi zawierać prawidłowy format adresu e-mail.
        /// </summary>
        [Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "Invalid email format.")]
		public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Adres fizyczny warsztatu.
        /// Pole jest obowiązkowe i musi zawierać od 1 do 50 znaków.
        /// </summary>
        [Required(ErrorMessage = "Address is required.")]
		[StringLength(50, MinimumLength = 1, ErrorMessage = "Brand must be between 1 and 50 characters.")]
		public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Numer telefonu warsztatu.
        /// Pole jest obowiązkowe i musi zawierać prawidłowy format numeru telefonu.
        /// </summary>
		[Required(ErrorMessage = "Phone Number is required.")]
		[Phone(ErrorMessage = "Invalid phone format")]
		public string PhoneNumber { get; set; } = string.Empty;
	}
}
