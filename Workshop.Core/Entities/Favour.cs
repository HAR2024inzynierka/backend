using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop.Core.Entities
{
	[Table("Uslugi")]
	public class Favour
	{
		public int Id { get; set; }
		[Column("Warsztat_id")]
		public int AutoServiceId { get; set; }
		[Column("RodzajUslugi", TypeName="varchar(50)")]
		public string TypeName { get; set; }
		[Column("Opis")]
		public string Description { get; set; } = string.Empty;
		[Column("Koszt")]
		public decimal Price { get; set; }
	}
}
