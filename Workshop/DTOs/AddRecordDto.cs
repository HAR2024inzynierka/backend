namespace Workshop.DTOs
{
	public class AddRecordDto//dobavti validaciju
	{
		public int VehicleId { get; set; }
		public int FavourId { get; set; }
		public int TermId { get; set; }
		public DateTime RecordDate { get; set; }
		public DateTime CompletionDate { get; set; }
	}
}
