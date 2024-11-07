using Microsoft.EntityFrameworkCore;
using Workshop.Core.Entities;
using Workshop.Core.Interfaces;
using Workshop.Infrastructure.Data;

namespace Workshop.Infrastructure.Repositories
{
    public class AutoRepairShopRepository : IAutoRepairShopRepository
	{
        private readonly WorkshopDbContext _context;

        public AutoRepairShopRepository (WorkshopDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AutoRepairShop autoRapairShop)
        {
            await _context.AutoRepairShops.AddAsync(autoRapairShop);
            await _context.SaveChangesAsync();
        }

		public async Task<List<AutoRepairShop>> GetAllAutoRepairShopsAsync()
        {
            return await _context.AutoRepairShops.ToListAsync();
        }
	}
}
