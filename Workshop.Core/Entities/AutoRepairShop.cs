using System.ComponentModel.DataAnnotations.Schema;

namespace Workshop.Core.Entities
{
    [Table("Warsztat")]
    public class AutoRepairShop
    {

        public int Id { get; set; }
        public string Email { get; set; }
        [Column("Adres")]
        public string Address {  get; set; }
        [Column("Telefon")]
        public string PhoneNumber { get; set; }
        
    }
}
