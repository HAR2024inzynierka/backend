using Workshop.Core.Entities;
using Workshop.Core.Interfaces;

namespace Workshop.Core.Services
{
	public class RecordService : IRecordService
	{
		private readonly IRecordRepository _recordRepository;
		public RecordService(IRecordRepository recordRepository)
		{
			_recordRepository = recordRepository;
		}

		public async Task AddRecordAsync(Record record)
		{
			await _recordRepository.AddRecordAsync(record);
		}
		public async Task<List<Record>> GetRecordsByUserIdAsync(int userId)
		{
			return await _recordRepository.GetRecordsByUserIdAsync(userId);
		}
	}
}
