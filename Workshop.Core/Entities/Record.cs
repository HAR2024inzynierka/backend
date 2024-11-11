using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop.Core.Entities
{
	[Table("Zapis")]
	public class Record
	{
		public int Id { get; set; }
		[Column("Pojazdy_id")]
		public int VehicleId { get; set; }
		[Column("Uslugi_id")]
		public int FavourId { get; set; }
		[Column("TerminySerwisowania_id")]
		public int TermId { get; set; }
		[Column("DataZapisu")]
		public DateTime RecordDate {  get; set; }
		[Column("DataUkonczenia")]
		public DateTime CompletionDate { get; set; }

		public Vehicle Vehicle { get; set; }  // Связь с автомобилем
	}
}
