using Workshop.Core.Entities;

namespace Workshop.Core.Interfaces
{
    /// <summary>
    /// Interfejs definiujący kontrakt dla repozytorium warsztatów samochodowych.
    /// Odpowiada za operacje związane z encją AutoRepairShop.
    /// </summary>
    public interface IAutoRepairShopRepository
    {
        /// <summary>
        /// Pobiera warsztat samochodowy na podstawie identyfikatora.
        /// </summary>
        /// <param name="id">Identyfikator warsztatu.</param>
        /// <returns>Obiekt AutoRepairShop lub null, jeśli nie znaleziono.</returns>
        Task<AutoRepairShop?> GetAutoRepairShopByIdAsync(int id);

        /// <summary>
        /// Dodaje nowy warsztat samochodowy do bazy danych.
        /// </summary>
        /// <param name="autoRepairShop">Obiekt AutoRepairShop do dodania.</param>
        Task AddAsync(AutoRepairShop autoRepairShop);

        /// <summary>
        /// Aktualizuje istniejący warsztat samochodowy w bazie danych.
        /// </summary>
        /// <param name="autoRepairShop">Obiekt AutoRepairShop z wprowadzonymi zmianami.</param>
        Task UpdateAsync(AutoRepairShop autoRepairShop);

        /// <summary>
        /// Usuwa warsztat samochodowy z bazy danych.
        /// </summary>
        /// <param name="autoRepairShop">Obiekt AutoRepairShop do usunięcia.</param>
        Task DeleteAsync(AutoRepairShop autoRepairShop);

        /// <summary>
        /// Pobiera listę wszystkich warsztatów samochodowych z bazy danych.
        /// </summary>
        /// <returns>Lista obiektów AutoRepairShop.</returns>
        Task<List<AutoRepairShop>> GetAllAutoRepairShopsAsync();

	}
}
