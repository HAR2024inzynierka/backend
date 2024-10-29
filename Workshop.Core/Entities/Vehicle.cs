using System.ComponentModel.DataAnnotations.Schema;

namespace Workshop.Core.Entities
{
    [Table("Pojazdy")]
    public class Vehicle
    {
        public int Id { get; set; }
        [Column("Marka")]
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        [Column("NrRejestracujny")]
        public string RegistrationNumber { get; set; } = string.Empty;
        [Column("Uzytkownicy_id")]
        public int UserId { get; set; }
    }
}
