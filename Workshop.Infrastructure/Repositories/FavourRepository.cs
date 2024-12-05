using Microsoft.EntityFrameworkCore;
using Workshop.Core.Entities;
using Workshop.Core.Interfaces;
using Workshop.Infrastructure.Data;

namespace Workshop.Infrastructure.Repositories
{
    /// <summary>
    /// Implementacja repozytorium dla usług warsztatu.
    /// Reprezentuje operacje na encji Favour w bazie danych.
    /// </summary>
    public class FavourRepository : IFavourRepository
	{
		private readonly WorkshopDbContext _context;

        /// <summary>
        /// Konstruktor repozytorium usług.
        /// Inicjalizuje kontekst bazy danych.
        /// </summary>
        /// <param name="context">Kontekst bazy danych WorkshopDbContext.</param>
        public FavourRepository(WorkshopDbContext context)
		{
			_context = context;
		}
		
		public async Task<Favour?> GetFavourByIdAsync(int id)
		{
			return await _context.Favours.FindAsync(id);	
		}

		public async Task AddFavourAsync(Favour favour)
		{
			await _context.Favours.AddAsync(favour);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateFavourAsync(Favour favour)
		{
			_context.Favours.Update(favour);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteFavourAsync(Favour favour)
		{
			_context.Favours.Remove(favour);
			await _context.SaveChangesAsync();
		}

		public async Task<List<Favour>> GetFavoursByAutoServiceIdAsync(int autoserviceId)
		{
			return await _context.Favours
				.Where(f => f.AutoRepairShopId == autoserviceId) // Filtruje usługi po identyfikatorze warsztatu
                .ToListAsync();
		}
	}
}
