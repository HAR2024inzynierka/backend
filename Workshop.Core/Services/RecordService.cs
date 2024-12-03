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

		public async Task<Record> GetRecordByIdAsync(int id)
		{
			var record = await _recordRepository.GetRecordByIdAsync(id);

			if (record == null)
			{
				throw new Exception("Record not found");
			}

            return record;
		}

		public async Task AddRecordAsync(Record record)
		{
			await _recordRepository.AddRecordAsync(record);
		}

		public async Task UpdateRecordAsync(Record updateRecord)
		{
            var record = await _recordRepository.GetRecordByIdAsync(updateRecord.Id);
            if (record == null)
            {
                throw new Exception("Record not found");
            }

            record.VehicleId = updateRecord.VehicleId;
			record.FavourId = updateRecord.FavourId;
			record.TermId = updateRecord.TermId;
			record.RecordDate = updateRecord.RecordDate;
			record.CompletionDate = updateRecord.CompletionDate;

            await _recordRepository.UpdateRecordAsync(record);
        }

        public async Task DeleteRecordAsync(int recordId)
        {
            var record = await _recordRepository.GetRecordByIdAsync(recordId);
            if (record == null)
            {
                throw new Exception("Record not found");
            }

            await _recordRepository.DeleteRecordAsync(record);
        }

        public async Task<List<Record>> GetRecordsByUserIdAsync(int userId)
		{
			return await _recordRepository.GetRecordsByUserIdAsync(userId);
		}

		public async Task<List<Record>> GetUncompletedRecordsAsync()
		{
			return await _recordRepository.GetUncompletedRecordsAsync();
		}

		public async Task CompleteRecordAsync(int recordId)
		{
			var record = await _recordRepository.GetRecordByIdAsync(recordId);

			if(record == null)
			{
				throw new Exception("Record not found");
			}
			else if(record.CompletionDate != null)
			{
				throw new Exception("Record has already been completed");
			}

			record.CompletionDate = DateTime.Now;

			await _recordRepository.UpdateRecordAsync(record);
		}

    }
}
