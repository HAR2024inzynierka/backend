﻿using Microsoft.EntityFrameworkCore;
using Workshop.Core.Entities;
using Workshop.Core.Interfaces;
using Workshop.Infrastructure.Data;

namespace Workshop.Infrastructure.Repositories
{
    /// <summary>
    /// Repozytorium do zarządzania terminami wizyt w systemie warsztatów samochodowych.
    /// Implementuje operacje na danych związanych z terminami wizyt.
    /// </summary>
    public class TermRepository : ITermRepository
	{
		private readonly WorkshopDbContext _context;

        /// <summary>
        /// Konstruktor klasy repozytorium. Używa kontekstu bazy danych do zarządzania danymi.
        /// </summary>
        /// <param name="context">Kontekst bazy danych</param>
        public TermRepository(WorkshopDbContext context)
		{
			_context = context;
		}

		public async Task<Term?> GetTermByIdAsync(int id)
		{
			return await _context.Terms.FindAsync(id);
		}

		public async Task AddTermAsync(Term term)
		{
			await _context.Terms.AddAsync(term);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateTermAsync(Term term)
		{
			_context.Terms.Update(term);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteTermAsync(Term term)
		{
			_context.Terms.Remove(term);
			await _context.SaveChangesAsync();
		}

		public async Task<List<Term>> GetTermsByAutoServiceIdAsync(int autoserviceId)
		{
			return await _context.Terms
				.Where(t => t.AutoServiceId == autoserviceId) // Filtruje po ID warsztatu
				.Where(t => t.Availability == true)
                .ToListAsync();
		}
	}
}
