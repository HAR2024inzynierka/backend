using Workshop.Core.Entities;
using Workshop.Core.Interfaces;

namespace Workshop.Core.Services
{
	public class AutoRepairShopService : IAutoRepairShopService
	{
		private readonly IAutoRepairShopRepository _autoRepairShopRepository;

		public AutoRepairShopService(IAutoRepairShopRepository autoRepairShopRepository)
		{
			_autoRepairShopRepository = autoRepairShopRepository;
		}

		public async Task<AutoRepairShop> GetAutoRepairShopByIdAsync(int id)
		{
			return await _autoRepairShopRepository.GetAutoRepairShopByIdAsync(id);
		}


        public async Task UpdateAsync(AutoRepairShop updateShop)
		{
            var autoRepairShop = await _autoRepairShopRepository.GetAutoRepairShopByIdAsync(updateShop.Id);
            if (autoRepairShop == null)
            {
                throw new Exception("Auto Repair Shop not found");
            }

            autoRepairShop.Email = updateShop.Email;
			autoRepairShop.Address = updateShop.Address;
			autoRepairShop.PhoneNumber = updateShop.PhoneNumber;

            await _autoRepairShopRepository.UpdateAsync(autoRepairShop);
        }
        public async Task DeleteAsync(int id)
        {
            var autoRepairShop = await _autoRepairShopRepository.GetAutoRepairShopByIdAsync(id);
            if (autoRepairShop == null)
            {
                throw new Exception("Auto Repair Shop not found");
            }

            await _autoRepairShopRepository.DeleteAsync(autoRepairShop);
        }

        public async Task<List<AutoRepairShop>> GetAllAutoRepairShopsAsync()
		{
			return await _autoRepairShopRepository.GetAllAutoRepairShopsAsync();
		}
	}
}
