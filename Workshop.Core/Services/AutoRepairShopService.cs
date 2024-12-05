using Workshop.Core.Entities;
using Workshop.Core.Interfaces;

namespace Workshop.Core.Services
{
    /// <summary>
    /// Serwis do zarządzania warsztatami samochodowymi.
    /// Zapewnia logikę biznesową, korzystając z repozytorium warsztatów.
    /// </summary>
	public class AutoRepairShopService : IAutoRepairShopService
	{
		private readonly IAutoRepairShopRepository _autoRepairShopRepository;

        /// <summary>
        /// Konstruktor usługi warsztatów.
        /// </summary>
        /// <param name="autoRepairShopRepository">Repozytorium warsztatów.</param>
		public AutoRepairShopService(IAutoRepairShopRepository autoRepairShopRepository)
		{
			_autoRepairShopRepository = autoRepairShopRepository;
		}

		public async Task<AutoRepairShop> GetAutoRepairShopByIdAsync(int id)
		{
            var autoRepairShop = await _autoRepairShopRepository.GetAutoRepairShopByIdAsync(id);

            // Jeśli warsztat nie został znaleziony, rzucamy wyjątek
            if (autoRepairShop == null)
            {
                throw new Exception("AutoRepeirShop not found");
            }

            return autoRepairShop;
		}


        public async Task UpdateAsync(AutoRepairShop updateShop)
		{
            var autoRepairShop = await _autoRepairShopRepository.GetAutoRepairShopByIdAsync(updateShop.Id);

            // Jeśli warsztat nie istnieje, rzucamy wyjątek
            if (autoRepairShop == null)
            {
                throw new Exception("Auto Repair Shop not found");
            }

            // Aktualizacja danych warsztatu
            autoRepairShop.Email = updateShop.Email;
			autoRepairShop.Address = updateShop.Address;
			autoRepairShop.PhoneNumber = updateShop.PhoneNumber;

            await _autoRepairShopRepository.UpdateAsync(autoRepairShop);
        }
        public async Task DeleteAsync(int id)
        {
            var autoRepairShop = await _autoRepairShopRepository.GetAutoRepairShopByIdAsync(id);

            // Jeśli warsztat nie istnieje, rzucamy wyjątek
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
