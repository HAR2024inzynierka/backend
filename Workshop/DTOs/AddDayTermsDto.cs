using System.ComponentModel.DataAnnotations;

namespace Workshop.DTOs
{
    public class AddDayTermsDto
    {
        [Required]
        public int AutoServiceId { get; set; }

        [Required]
        public DateTime Day { get; set; }
    }
}
