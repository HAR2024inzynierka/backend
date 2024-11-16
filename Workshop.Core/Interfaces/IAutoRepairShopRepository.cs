using Workshop.Core.Entities;

namespace Workshop.Core.Interfaces
{
    public interface IAutoRepairShopRepository
    {
        Task<AutoRepairShop> GetAutoRepairShopByIdAsync(int id);
        Task AddAsync(AutoRepairShop autoRapairShop);
        Task UpdateAsync(AutoRepairShop autoRapairShop);
        Task DeleteAsync(AutoRepairShop autoRapairShop);
        Task<List<AutoRepairShop>> GetAllAutoRepairShopsAsync();

	}
}
