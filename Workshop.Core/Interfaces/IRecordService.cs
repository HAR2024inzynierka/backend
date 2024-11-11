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
		Task AddRecordAsync(Record record);
		Task<List<Record>> GetRecordsByUserIdAsync(int userId);
	}
}
