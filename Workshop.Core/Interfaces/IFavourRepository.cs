using Workshop.Core.Entities;

namespace Workshop.Core.Interfaces
{
    /// <summary>
    /// Interfejs definiujący operacje na encji Favour.
    /// Reprezentuje usługi oferowane przez warsztaty samochodowe.
    /// </summary>
    public interface IFavourRepository
	{
        /// <summary>
        /// Pobiera szczegóły usługi na podstawie jej identyfikatora.
        /// </summary>
        /// <param name="id">Identyfikator usługi.</param>
        /// <returns>Obiekt Favour lub null, jeśli nie znaleziono.</returns>
        Task<Favour?> GetFavourByIdAsync(int id);

        /// <summary>
        /// Dodaje nową usługę do bazy danych.
        /// </summary>
        /// <param name="favour">Obiekt Favour do dodania.</param>
		Task AddFavourAsync(Favour favour);

        /// <summary>
        /// Aktualizuje istniejącą usługę w bazie danych.
        /// </summary>
        /// <param name="favour">Obiekt Favour z wprowadzonymi zmianami.</param>
		Task UpdateFavourAsync(Favour favour);

        /// <summary>
        /// Usuwa usługę z bazy danych.
        /// </summary>
        /// <param name="favour">Obiekt Favour do usunięcia.</param>
		Task DeleteFavourAsync(Favour favour);

        /// <summary>
        /// Pobiera listę usług oferowanych przez konkretny warsztat samochodowy.
        /// </summary>
        /// <param name="autoserviceId">Identyfikator warsztatu samochodowego.</param>
        /// <returns>Lista obiektów Favour.</returns>
		Task<List<Favour>> GetFavoursByAutoServiceIdAsync(int autoserviceId);
	}
}
