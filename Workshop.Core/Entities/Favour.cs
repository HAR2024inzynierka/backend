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

		public enum FavourType
		{
			[Display(Name = "Wymiana oleju silnikowego")]
			WymianaOlejuSilnikowego,

			[Display(Name = "Wymiana opon")]
			WymianaOpon,

			[Display(Name = "Wymiana pasu alternatora")]
			WymianaPasuAlternatora,

			[Display(Name = "Wymiana filtru olejowego")]
			WymianaFiltruOlejowego,

			[Display(Name = "Wymiana filtru powietrznego")]
			WymianaFiltruPowietrznego,

			[Display(Name = "Wymiana filtru kabinowego")]
			WymianaFiltruKabinowego,

			[Display(Name = "Wymiana filtru paliwowego")]
			WymianaFiltruPaliwowego,

			[Display(Name = "Wymiana płynu hamulcowego")]
			WymianaPlynuHamulcowego,

			[Display(Name = "Wymiana płynu chłodzącego")]
			WymianaPlynuChlodzacego,

			[Display(Name = "Wymiana układu wspomagania kierownicy")]
			WymianaUkladuWspomaganiaKierownicy,

			[Display(Name = "Wymiana układu hamulcowego")]
			WymianaUkladuHamulcowego,

			[Display(Name = "Wymiana sprzęgła")]
			WymianaSprzegla,

			[Display(Name = "Wymiana klocków hamulcowych")]
			WymianaKlockowHamulcowych,

			[Display(Name = "Wymiana świec zapłonowych")]
			WymianaSwiecZaplonowych,

			[Display(Name = "Naprawa automatycznej skrzyni biegów")]
			NaprawaAutomatycznejSkrzyniBiegow,

			[Display(Name = "Naprawa manualnej skrzyni biegów")]
			NaprawaManualnejSkrzyniBiegow,

			[Display(Name = "Badania techniczne pojazdu")]
			BadaniaTechnicznePojazdu
		}
	}
}
