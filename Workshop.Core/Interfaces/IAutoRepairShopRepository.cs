using Workshop.Core.Entities;

namespace Workshop.Core.Interfaces
{
    internal interface IAutoRepairShopRepository
    {
        Task AddAsync(AutoRepairShop autoRapairShop);
    }
}
