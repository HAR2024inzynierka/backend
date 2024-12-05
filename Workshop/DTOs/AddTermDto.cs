using System.ComponentModel.DataAnnotations;

namespace Workshop.DTOs
{
    /// <summary>
    /// Data transfer object (DTO) używane do dodawania nowych terminów w systemie.
    /// </summary>
    public class AddTermDto
	{
        /// <summary>
        /// Data i godzina rozpoczęcia terminu.
        /// Pole jest obowiązkowe i musi zawierać prawidłową datę i czas.
        /// </summary>
        [Required(ErrorMessage = "Start Date is required.")]
		[DataType(DataType.DateTime, ErrorMessage = "Start Date must be a valid date and time.")]
		public DateTime StartDate { get; set; }

        /// <summary>
        /// Data i godzina zakończenia terminu.
        /// Pole jest obowiązkowe i musi zawierać prawidłową datę i czas.
        /// </summary>
        [Required(ErrorMessage = "End Date is required.")]
		[DataType(DataType.DateTime, ErrorMessage = "End Date must be a valid date and time.")]
		public DateTime EndDate { get; set; }

        /// <summary>
        /// Wskaźnik dostępności terminu.
        /// Określa, czy termin jest dostępny w systemie.
        /// </summary>
		[Required(ErrorMessage = "Availability is required.")]
		public bool Availability { get; set; }

        /// <summary>
        /// Identyfikator warsztatu, do którego przypisany jest termin.
        /// Musi być liczbą całkowitą większą niż zero.
        /// </summary>
		[Required(ErrorMessage = "AutoServiceId is required.")]
		[Range(1, int.MaxValue, ErrorMessage = "AutoServiceId must be a positive integer.")]
		public int AutoServiceId { get; set; }

	}
}
