using Workshop.Core.Entities;
using Workshop.Core.Interfaces;

namespace Workshop.Core.Services
{
    /// <summary>
    /// Serwis zarządzający rekordami wizyt w warsztacie.
    /// </summary>
    public class RecordService : IRecordService
	{
		private readonly IRecordRepository _recordRepository;

        /// <summary>
        /// Konstruktor serwisu RecordService.
        /// </summary>
        /// <param name="recordRepository">Repozytorium operujące na danych rekordów</param>
        public RecordService(IRecordRepository recordRepository)
		{
			_recordRepository = recordRepository;
		}

		public async Task<Record> GetRecordByIdAsync(int id)
		{
			var record = await _recordRepository.GetRecordByIdAsync(id);

            // Jeśli rekord nie istnieje, rzucamy wyjątek
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

            // Jeśli rekord nie istnieje, rzucamy wyjątek
            if (record == null)
            {
                throw new Exception("Record not found");
            }

            // Aktualizujemy dane rekordu
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

            // Jeśli rekord nie istnieje, rzucamy wyjątek
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

            // Jeśli rekord nie istnieje, rzucamy wyjątek
            if (record == null)
			{
				throw new Exception("Record not found");
			}
            // Jeśli rekord ma już datę zakończenia, rzucamy wyjątek, bo nie można go zakończyć ponownie
            else if (record.CompletionDate != null)
			{
				throw new Exception("Record has already been completed");
			}

            // Ustawiamy datę zakończenia wizyty
            record.CompletionDate = DateTime.Now;

			await _recordRepository.UpdateRecordAsync(record);
		}

    }
}
