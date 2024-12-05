using System.ComponentModel.DataAnnotations;

namespace Workshop.DTOs
{
    /// <summary>
    /// Data transfer object (DTO) używane do dodawania nowych zapisów w systemie.
    /// </summary>
	public class AddRecordDto
	{
        /// <summary>
        /// Identyfikator pojazdu, który jest przypisany do zapisu.
        /// Musi być liczbą całkowitą większą niż zero.
        /// </summary>
        [Required(ErrorMessage = "VehicleId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "VehicleId must be a positive integer.")]
        public int VehicleId { get; set; }

        /// <summary>
        /// Identyfikator usługi, która jest przypisana do zapisu.
        /// Musi być liczbą całkowitą większą niż zero.
        /// </summary>
        [Required(ErrorMessage = "FavourId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "FavourId must be a positive integer.")]
        public int FavourId { get; set; }

        /// <summary>
        /// Identyfikator terminu, który jest przypisany do zapisu.
        /// Musi być liczbą całkowitą większą niż zero.
        /// </summary>
        [Required(ErrorMessage = "TermId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "TermId must be a positive integer.")]
        public int TermId { get; set; }
	}
}
