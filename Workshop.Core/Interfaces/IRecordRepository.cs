using Workshop.Core.Entities;

namespace Workshop.Core.Interfaces
{
	public interface IRecordRepository
	{
		Task<Record?> GetRecordByIdAsync(int id);
		Task AddRecordAsync(Record record);
		Task UpdateRecordAsync(Record record);
		Task DeleteRecordAsync(Record record);
		Task<List<Record>> GetRecordsByUserIdAsync(int userId);
		Task<List<Record>> GetUncompletedRecordsAsync();
	}
}
