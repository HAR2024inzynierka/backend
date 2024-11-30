using System.ComponentModel.DataAnnotations;

namespace Workshop.DTOs
{
	public class AddFavourDto
	{
		[Required(ErrorMessage = "TypeName is required.")]
		public required string TypeName { get; set; }
		[Required(ErrorMessage = "Description is required.")]
		[StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
		public required string Description { get; set; }
		[Required(ErrorMessage = "Price is required.")]
		[Range(0.01, 10000, ErrorMessage = "Price must be between 0.01 and 10,000.")]
		public decimal Price { get; set; }
		[Required(ErrorMessage = "AutoServiceId is required.")]
		[Range(1, int.MaxValue, ErrorMessage = "AutoServiceId must be a positive integer.")]
		public int AutoServiceId { get; set; }
	}
}
