using System.ComponentModel.DataAnnotations;
using System;

namespace Workshop.DTOs
{
	public class AddTermDto
	{
		[Required(ErrorMessage = "Start Date is required.")]
		[DataType(DataType.DateTime, ErrorMessage = "Start Date must be a valid date and time.")]
		public DateTime StartDate { get; set; }
		[Required(ErrorMessage = "End Date is required.")]
		[DataType(DataType.DateTime, ErrorMessage = "End Date must be a valid date and time.")]
		public DateTime EndDate { get; set; }
		[Required(ErrorMessage = "Availability is required.")]
		public bool Availability { get; set; }
		[Required(ErrorMessage = "AutoServiceId is required.")]
		[Range(1, int.MaxValue, ErrorMessage = "AutoServiceId must be a positive integer.")]
		public int AutoServiceId { get; set; }

	}
}
