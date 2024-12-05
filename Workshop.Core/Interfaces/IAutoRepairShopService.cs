using Workshop.Core.Entities;

namespace Workshop.Core.Interfaces
{
    /// <summary>
    /// Interfejs dla serwisu warsztatu samochodowego, oferujący operacje na warsztatach.
    /// </summary>
    public interface IAutoRepairShopService
	{
        /// <summary>
        /// Asynchronicznie pobiera warsztat samochodowy na podstawie ID.
        /// </summary>
        /// <param name="id">Identyfikator warsztatu.</param>
        /// <returns>Obiekt AutoRepairShop</returns>
        Task<AutoRepairShop> GetAutoRepairShopByIdAsync(int id);
        /// <summary>
        /// Asynchronicznie aktualizuje dane istniejącego warsztatu samochodowego.
        /// </summary>
        /// <param name="autoRepairShop">Obiekt AutoRepairShop zawierający zaktualizowane informacje.</param>
        Task UpdateAsync(AutoRepairShop autoRepairShop);
        /// <summary>
        /// Asynchronicznie usuwa warsztat samochodowy na podstawie unikalnego ID.
        /// </summary>
        /// <param name="id">Identyfikator warsztatu, który ma zostać usunięty.</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Asynchronicznie pobiera wszystkie warsztaty samochodowe.
        /// </summary>
        /// <returns>Wynik zawiera listę wszystkich warsztatów samochodowych.</returns>
        Task<List<AutoRepairShop>> GetAllAutoRepairShopsAsync();

	}
}
