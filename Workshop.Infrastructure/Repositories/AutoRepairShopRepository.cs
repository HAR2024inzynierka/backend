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

        public async Task<AutoRepairShop?> GetAutoRepairShopByIdAsync(int id)
        {
            return await _context.AutoRepairShops.FindAsync(id);
        }

        public async Task AddAsync(AutoRepairShop autoRapairShop)
        {
            await _context.AutoRepairShops.AddAsync(autoRapairShop);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AutoRepairShop autoRepairShop)
        {
            _context.AutoRepairShops.Update(autoRepairShop);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AutoRepairShop autoRepairShop)
        {
            _context.AutoRepairShops.Remove(autoRepairShop);
            await _context.SaveChangesAsync();
        }

		public async Task<List<AutoRepairShop>> GetAllAutoRepairShopsAsync()
        {
            return await _context.AutoRepairShops.ToListAsync();
        }
	}
}
