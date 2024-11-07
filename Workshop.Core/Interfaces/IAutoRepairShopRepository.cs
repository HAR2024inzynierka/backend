using Workshop.Core.Entities;

namespace Workshop.Core.Interfaces
{
    public interface IAutoRepairShopRepository
    {
        Task AddAsync(AutoRepairShop autoRapairShop);
        Task<List<AutoRepairShop>> GetAllAutoRepairShopsAsync();

	}
}
