using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop.Core.Entities;
using Workshop.Core.Interfaces;
using Workshop.Infrastructure.Data;

namespace Workshop.Infrastructure.Repositories
{
	public class RecordRepository : IRecordRepository
	{
		private readonly WorkshopDbContext _context;

		public RecordRepository(WorkshopDbContext context)
		{
			_context = context;
		}
		public async Task<Record> GetRecordByIdAsync(int id)
		{
			return await _context.Records.FindAsync(id);
		}

		public async Task AddRecordAsync(Record record)
		{
			await _context.Records.AddAsync(record);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateRecordAsync(Record record)
		{
			_context.Records.Update(record);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteRecordAsync(Record record)
		{
			_context.Records.Remove(record);
			await _context.SaveChangesAsync();
		}

		public async Task<List<Record>> GetRecordsByUserIdAsync(int userId)
		{
			return await _context.Records
				.Include(r => r.Vehicle)
				.Where(r => r.Vehicle.UserId == userId).ToListAsync();
		}
	}
}
