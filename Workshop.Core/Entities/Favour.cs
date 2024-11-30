using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Workshop.Core.Entities
{
	[Table("Uslugi")]
	public class Favour
	{
		public int Id { get; set; }
		[Column("Warsztat_id")]
		public int AutoRepairShopId { get; set; }
		[Column("RodzajUslugi", TypeName="varchar(50)")]
		public required string TypeName { get; set; }
		[Column("Opis")]
		public string Description { get; set; } = string.Empty;
		[Column("Koszt")]
		public decimal Price { get; set; }
		public AutoRepairShop AutoRepairShop { get; set; } = null!;
    }
}
