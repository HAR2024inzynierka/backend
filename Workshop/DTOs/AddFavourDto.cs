using System.ComponentModel.DataAnnotations;

namespace Workshop.DTOs
{
    /// <summary>
    /// Data transfer object (DTO) używane do dodawania nowych usług w systemie. 
    /// </summary>
    public class AddFavourDto
	{
        /// <summary>
        /// Typ usługi.
        /// </summary>
        [Required(ErrorMessage = "TypeName is required.")]
		public required string TypeName { get; set; }

        /// <summary>
        /// Opis usługi.
        /// Długość opisu nie powinien przekraczać 500 znaków.
        /// </summary>
        [Required(ErrorMessage = "Description is required.")]
		[StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
		public required string Description { get; set; }

        /// <summary>
        /// Cena usługi.
        /// Cena musi być większa niż 0 i mniejsza niż 10 000.
        /// </summary>
		[Required(ErrorMessage = "Price is required.")]
		[Range(0.01, 10000, ErrorMessage = "Price must be between 0.01 and 10,000.")]
		public decimal Price { get; set; }

        /// <summary>
        /// Identyfikator warsztatu, do którego przypisana jest usługa.
        /// Musi być liczbą całkowitą większą od zera.
        /// </summary>
		[Required(ErrorMessage = "AutoServiceId is required.")]
		[Range(1, int.MaxValue, ErrorMessage = "AutoServiceId must be a positive integer.")]
		public int AutoServiceId { get; set; }
	}
}
