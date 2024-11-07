using System.ComponentModel.DataAnnotations;

namespace Workshop.DTOs
{
	public class AutoRepairShopDto
	{
		[Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "Invalid email format.")]
		public string Email { get; set; } = string.Empty;
		[Required(ErrorMessage = "Address is required.")]
		[StringLength(50, MinimumLength = 1, ErrorMessage = "Brand must be between 1 and 50 characters.")]
		public string Address { get; set; } = string.Empty;
		[Required(ErrorMessage = "Phone Number is required.")]
		[Phone(ErrorMessage = "Invalid phone format")]
		public string PhoneNumber { get; set; } = string.Empty;
	}
}
