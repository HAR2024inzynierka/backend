using System.ComponentModel.DataAnnotations.Schema;

namespace Workshop.Core.Entities
{
    /// <summary>
    /// Reprezentuje warsztat samochodowy w systemie.
    /// </summary>
    [Table("Warsztat")]
    public class AutoRepairShop
    {
        /// <summary>
        /// Unikalny identyfikator warsztatu.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Adres e-mail warsztatu.
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Adres warsztatu.
        /// </summary>
        [Column("Adres")]
        public required string Address {  get; set; }

        /// <summary>
        /// Numer telefonu warsztatu.
        /// </summary>
        [Column("Telefon")]
        public required string PhoneNumber { get; set; }
        
    }
}
