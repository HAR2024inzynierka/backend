using System.ComponentModel.DataAnnotations.Schema;

namespace Workshop.Core.Entities
{
    [Table("Warsztat")]
    public class AutoRepairShop
    {

        public int Id { get; set; }
        public required string Email { get; set; }
        [Column("Adres")]
        public required string Address {  get; set; }
        [Column("Telefon")]
        public required string PhoneNumber { get; set; }
        
    }
}
