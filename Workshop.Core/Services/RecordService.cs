﻿using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Query;
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
			return await _recordRepository.GetRecordByIdAsync(id);
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
	}
}
