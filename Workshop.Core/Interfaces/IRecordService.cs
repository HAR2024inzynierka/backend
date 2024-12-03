using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop.Core.Entities;

namespace Workshop.Core.Interfaces
{
	public interface IRecordService
	{
		Task<Record> GetRecordByIdAsync(int id);
		Task AddRecordAsync(Record record);
		Task UpdateRecordAsync(Record record);
		Task DeleteRecordAsync(int id);
		Task<List<Record>> GetRecordsByUserIdAsync(int userId);
        Task<List<Record>> GetUncompletedRecordsAsync();
		Task CompleteRecordAsync(int recordId);
    }
}
