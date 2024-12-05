using System.ComponentModel.DataAnnotations.Schema;

namespace Workshop.Core.Entities
{
    /// <summary>
    /// Reprezentuje zapis na usługę w warsztacie samochodowym.
    /// </summary>
    [Table("Zapis")]
	public class Record
	{
        /// <summary>
        /// Unikalny identyfikator zapisu.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identyfikator pojazdu, który jest związany z zapisem.
        /// </summary>
        [Column("Pojazdy_id")]
		public int VehicleId { get; set; }

        /// <summary>
        /// Identyfikator usługi przypisanej do zapisu.
        /// </summary>
        [Column("Uslugi_id")]
		public int FavourId { get; set; }

        /// <summary>
        /// Identyfikator terminu przypisanego do zapisu.
        /// </summary>
        [Column("TerminySerwisowania_id")]
		public int TermId { get; set; }

        /// <summary>
        /// Data, kiedy zapis został dokonany.
        /// </summary>
        [Column("DataZapisu")]
		public DateTime RecordDate {  get; set; }

        /// <summary>
        /// Data ukończenia usługi (opcjonalna, ponieważ usługa może nie być jeszcze zakończona).
        /// </summary>
        [Column("DataUkonczenia")]
		public DateTime? CompletionDate { get; set; }


        /// <summary>
        /// Obiekt pojazdu powiązanego z zapisem.
        /// </summary>
		public Vehicle Vehicle { get; set; } = null!;

        /// <summary>
        /// Obiekt usługi powiązanej z zapisem.
        /// </summary>
		public Favour Favour { get; set; } = null!;

        /// <summary>
        /// Obiekt terminu powiązanego z zapisem.
        /// </summary>
        public Term Term { get; set; } = null!;
    }
}
