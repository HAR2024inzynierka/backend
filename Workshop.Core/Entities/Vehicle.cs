using System.ComponentModel.DataAnnotations.Schema;

namespace Workshop.Core.Entities
{
    /// <summary>
    /// Reprezentuje pojazd użytkownika w systemie. 
	/// </summary>
    [Table("Pojazdy")]
	public class Vehicle
	{
        /// <summary>
        /// Unikalny identyfikator pojazdu.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Marka pojazdu.
        /// </summary>
        [Column("Marka")]
		public string Brand { get; set; } = string.Empty;

        /// <summary>
        /// Model pojazdu.
        /// </summary>
        public string Model { get; set; } = string.Empty;

        /// <summary>
        /// Numer rejestracyjny pojazdu.
        /// </summary>
        [Column("NrRejestracujny")]
		public string RegistrationNumber { get; set; } = string.Empty;

        /// <summary>
        /// Pojemność silnika pojazdu (w centymetrach sześciennych).
        /// </summary>
		[Column("Pojemnosc")]
		public int Capacity { get; set; }

        /// <summary>
        /// Moc silnika pojazdu (w koniach mechanicznych).
        /// </summary>
		[Column("Moc")]
		public int Power { get; set; }

        /// <summary>
        /// Numer identyfikacyjny pojazdu (VIN).
        /// </summary>
		public string VIN { get; set; } = string.Empty;

        /// <summary>
        /// Rok produkcji pojazdu.
        /// </summary>
		[Column("RokProdukcji")]
		public int ProductionYear { get; set; }

        /// <summary>
        /// Identyfikator użytkownika, któremu pojazd jest przypisany.
        /// </summary>
		[Column("Uzytkownicy_id")]
		public int UserId { get; set; }
	}
}
