using Workshop.Core.Entities;

namespace Workshop.Core.Interfaces
{
	public interface IRecordRepository
	{
		Task AddRecordAsync(Record record);
		Task<List<Record>> GetRecordsByUserIdAsync(int userId);
	}
}
