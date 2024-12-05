using System.ComponentModel.DataAnnotations;

namespace Workshop.DTOs
{
    /// <summary>
    /// Data transfer object (DTO) używane do reprezentowania danych pojazdu.
    /// </summary>
    public class VehicleDto
    {
        /// <summary>
        /// Marka pojazdu. 
        /// Pole jest obowiązkowe i musi zawierać od 1 do 50 znaków.
        /// </summary>
        [Required(ErrorMessage = "Brand is required.")]
		[StringLength(50, MinimumLength = 1, ErrorMessage = "Brand must be between 1 and 50 characters.")]
		public required string Brand { get; set; }

        /// <summary>
        /// Model pojazdu.
        /// Pole jest obowiązkowe i musi zawierać od 1 do 50 znaków.
        /// </summary>
        [Required(ErrorMessage = "Model is required.")]
		[StringLength(50, MinimumLength = 1, ErrorMessage = "Model must be between 1 and 50 characters.")]
		public required string Model { get; set; }

        /// <summary>
        /// Numer rejestracyjny pojazdu.
        /// Pole jest obowiązkowe i musi być alfanumeryczne, o długości od 1 do 10 znaków.
        /// </summary>
        [Required(ErrorMessage = "Registration Number is required.")]
		[RegularExpression(@"^[A-Za-z0-9\s-]{1,10}$", ErrorMessage = "Registration number must be alphanumeric and 1 to 10 characters.")]
		public required string RegistrationNumber { get; set; }

        /// <summary>
        /// Pojemność silnika pojazdu.
        /// Pole jest obowiązkowe i musi mieścić się w przedziale od 1 do 10 000 cc.
        /// </summary>
		[Required(ErrorMessage = "Capacity is required.")]
		[Range(1, 10000, ErrorMessage = "Capacity must be between 1 and 10,000 cc.")]
		public int Capacity { get; set; }

        /// <summary>
        /// Moc silnika pojazdu.
        /// Pole jest obowiązkowe i musi mieścić się w przedziale od 1 do 2000 KM.
        /// </summary>
		[Required(ErrorMessage = "Power is required.")]
		[Range(1, 2000, ErrorMessage = "Power must be between 1 and 2000 hp.")]
		public int Power { get; set; }

        /// <summary>
        /// Numer identyfikacyjny pojazdu (VIN).
        /// Pole jest obowiązkowe, musi mieć dokładnie 17 znaków alfanumerycznych (wykluczając litery I, O i Q).
        /// </summary>
		[Required(ErrorMessage = "VIN is required.")]
		[StringLength(17, MinimumLength = 17, ErrorMessage = "VIN must be exactly 17 characters.")]
		[RegularExpression(@"^[A-HJ-NPR-Z0-9]{17}$", ErrorMessage = "VIN must be alphanumeric, 17 characters, excluding I, O, and Q.")]
		public required string VIN { get; set; }

        /// <summary>
        /// Rok produkcji pojazdu.
        /// Pole jest obowiązkowe i musi mieścić się w przedziale od 1886 do 2100.
        /// </summary>
		[Required(ErrorMessage = "Production Year is required.")]
		[Range(1886, 2100, ErrorMessage = "Production year must be between 1886 and 2100.")]
		public int ProductionYear { get; set; }
	}
}
