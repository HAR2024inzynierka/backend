using System.ComponentModel.DataAnnotations;

namespace Workshop.DTOs
{
	public class AddRecordDto
	{
        [Required(ErrorMessage = "VehicleId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "VehicleId must be a positive integer.")]
        public int VehicleId { get; set; }
        [Required(ErrorMessage = "FavourId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "FavourId must be a positive integer.")]
        public int FavourId { get; set; }
        [Required(ErrorMessage = "TermId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "TermId must be a positive integer.")]
        public int TermId { get; set; }
        [Required(ErrorMessage = "RecordDate is required.")]
        public DateTime RecordDate { get; set; }
        public DateTime? CompletionDate { get; set; }
	}
}
