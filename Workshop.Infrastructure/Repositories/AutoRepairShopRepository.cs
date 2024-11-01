using Workshop.Core.Entities;
using Workshop.Infrastructure.Data;

namespace Workshop.Infrastructure.Repositories
{
    public class AutoRepairShopRepository
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
    }
}
