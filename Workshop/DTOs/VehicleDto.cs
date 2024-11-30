using System.ComponentModel.DataAnnotations;

namespace Workshop.DTOs
{
    public class VehicleDto
    {
		[Required(ErrorMessage = "Brand is required.")]
		[StringLength(50, MinimumLength = 1, ErrorMessage = "Brand must be between 1 and 50 characters.")]
		public required string Brand { get; set; }

		[Required(ErrorMessage = "Model is required.")]
		[StringLength(50, MinimumLength = 1, ErrorMessage = "Model must be between 1 and 50 characters.")]
		public required string Model { get; set; }

		[Required(ErrorMessage = "Registration Number is required.")]
		[RegularExpression(@"^[A-Za-z0-9\s-]{1,10}$", ErrorMessage = "Registration number must be alphanumeric and 1 to 10 characters.")]
		public required string RegistrationNumber { get; set; }

		[Required(ErrorMessage = "Capacity is required.")]
		[Range(1, 10000, ErrorMessage = "Capacity must be between 1 and 10,000 cc.")]
		public int Capacity { get; set; }

		[Required(ErrorMessage = "Power is required.")]
		[Range(1, 2000, ErrorMessage = "Power must be between 1 and 2000 hp.")]
		public int Power { get; set; }

		[Required(ErrorMessage = "VIN is required.")]
		[StringLength(17, MinimumLength = 17, ErrorMessage = "VIN must be exactly 17 characters.")]
		[RegularExpression(@"^[A-HJ-NPR-Z0-9]{17}$", ErrorMessage = "VIN must be alphanumeric, 17 characters, excluding I, O, and Q.")]
		public required string VIN { get; set; }

		[Required(ErrorMessage = "Production Year is required.")]
		[Range(1886, 2100, ErrorMessage = "Production year must be between 1886 and 2100.")]
		public int ProductionYear { get; set; }
	}
}
