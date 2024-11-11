

using System.ComponentModel.DataAnnotations.Schema;

namespace Workshop.Core.Entities
{
	[Table("TerminySerwisowania")]
	public class Term
	{
		public int Id { get; set; }
		[Column("Warsztat_id")]
		public int AutoServiceId { get; set; }
		[Column("DataRozpoczecia")]
		public DateTime StartDate { get; set; }
		[Column("DataUkonczenia")]
		public DateTime EndDate { get; set; }
		[Column("Dostepnosc")]
		public bool Availability { get; set; }
	}
}
