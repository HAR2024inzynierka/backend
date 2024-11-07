using System.ComponentModel.DataAnnotations;

namespace Workshop.DTOs
{
    public class VehicleDto
    {
        [Required(ErrorMessage = "Brand is required.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Brand must be between 1 and 50 characters.")]
        public string Brand { get; set; } = string.Empty;
        [Required(ErrorMessage = "Model is required.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Brand must be between 1 and 50 characters.")]
        public string Model { get; set; } = string.Empty;
        [Required(ErrorMessage = "Registration Number is required.")]
        public string RegistrationNumber { get; set; } = string.Empty;

    }
}
