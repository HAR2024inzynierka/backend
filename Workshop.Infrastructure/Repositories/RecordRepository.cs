using Microsoft.EntityFrameworkCore;
using Workshop.Core.Entities;
using Workshop.Core.Interfaces;
using Workshop.Infrastructure.Data;

namespace Workshop.Infrastructure.Repositories
{
    /// <summary>
    /// Repozytorium operujące na rekordach (wizytach) w systemie warsztatów samochodowych.
    /// Reprezentuje operacje na encji Record w bazie danych.
    /// </summary>
    public class RecordRepository : IRecordRepository
	{
		private readonly WorkshopDbContext _context;

        /// <summary>
        /// Konstruktor repozytorium, który wstrzykuje kontekst bazy danych.
        /// </summary>
        /// <param name="context">Kontekst bazy danych.</param>
        public RecordRepository(WorkshopDbContext context)
		{
			_context = context;
		}
		public async Task<Record?> GetRecordByIdAsync(int id)
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
				.Include(r => r.Vehicle) // Załączamy pojazdy do rekordu
                .Include(r => r.Favour) // Załączamy usługi do rekordu
                    .ThenInclude(f => f.AutoRepairShop) // Załączamy warsztaty do usług
                .Include(r => r.Term) // Załączamy terminy do rekordu
                .Where(r => r.Vehicle.UserId == userId) // Filtruje rekordy po użytkowniku
                .ToListAsync();
		}

		public async Task<List<Record>> GetUncompletedRecordsAsync()
		{
			return await _context.Records
                .Include(r => r.Vehicle) // Załączamy pojazdy do rekordu
                .Include(r => r.Favour) // Załączamy usługi do rekordu
                    .ThenInclude(f => f.AutoRepairShop) // Załączamy warsztaty do usług
                .Include(r => r.Term) // Załączamy terminy do rekordu
                .Where(r => r.CompletionDate == null) // Filtruje rekordy, które nie zostały ukończone
                .ToListAsync();
        }
	}
}
