using System.ComponentModel.DataAnnotations.Schema;

namespace Workshop.Core.Entities
{
    /// <summary>
    /// Reprezentuje termin serwisowania w warsztacie samochodowym.
    /// </summary>
    [Table("TerminySerwisowania")]
	public class Term
	{
        /// <summary>
        /// Unikalny identyfikator terminu.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identyfikator warsztatu, do którego należy ten termin.
        /// </summary>
        [Column("Warsztat_id")]
		public int AutoServiceId { get; set; }

        /// <summary>
        /// Data rozpoczęcia terminu serwisowego.
        /// </summary>
		[Column("DataRozpoczecia")]
		public DateTime StartDate { get; set; }

        /// <summary>
        /// Data zakończenia terminu serwisowego.
        /// </summary>
		[Column("DataUkonczenia")]
		public DateTime EndDate { get; set; }

        /// <summary>
        /// Informacja o dostępności terminu (true - dostępny, false - niedostępny).
        /// </summary>
		[Column("Dostepnosc")]
		public bool Availability { get; set; }
	}
}
