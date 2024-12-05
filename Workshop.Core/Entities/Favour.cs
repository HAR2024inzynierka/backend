using System.ComponentModel.DataAnnotations.Schema;


namespace Workshop.Core.Entities
{
    /// <summary>
    /// Reprezentuje usługę oferowaną przez warsztat samochodowy.
    /// </summary>
    [Table("Uslugi")]
	public class Favour
	{
        /// <summary>
        /// Unikalny identyfikator usługi.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identyfikator warsztatu, który oferuje usługę.
        /// </summary>
        [Column("Warsztat_id")]
		public int AutoRepairShopId { get; set; }

        /// <summary>
        /// Nazwa lub rodzaj usługi, np. "Naprawa silnika", "Wymiana opon".
        /// </summary>
		[Column("RodzajUslugi", TypeName="varchar(50)")]
		public required string TypeName { get; set; }

        /// <summary>
        /// Szczegółowy opis usługi.
        /// </summary>
		[Column("Opis")]
		public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Cena usługi.
        /// </summary>
		[Column("Koszt")]
		public decimal Price { get; set; }


        /// <summary>
        /// Powiązany warsztat, który oferuje daną usługę.
        /// </summary>
		public AutoRepairShop AutoRepairShop { get; set; } = null!;
    }
}
