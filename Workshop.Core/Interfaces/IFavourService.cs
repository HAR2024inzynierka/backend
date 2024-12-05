using Workshop.Core.Entities;

namespace Workshop.Core.Interfaces
{
    /// <summary>
    /// Interfejs serwisu odpowiedzialnego za operacje związane z usługami oferowanymi przez warsztaty.
    /// </summary>
    public interface IFavourService
	{
        /// <summary>
        /// Pobiera usługę na podstawie jej identyfikatora.
        /// </summary>
        /// <param name="id">Identyfikator usługi</param>
        /// <returns>Obiekt typu Favour</returns>
        Task<Favour> GetFavourByIdAsync(int id);

        /// <summary>
        /// Dodaje nową usługę do systemu.
        /// </summary>
        /// <param name="favour">Obiekt usługi do dodania</param>
        Task AddFavourAsync(Favour favour);

        /// <summary>
        /// Aktualizuje istniejącą usługę w systemie.
        /// </summary>
        /// <param name="favour">Obiekt usługi z nowymi danymi</param>
		Task UpdateFavourAsync(Favour favour);

        /// <summary>
        /// Usuwa usługę na podstawie jej identyfikatora.
        /// </summary>
        /// <param name="id">Identyfikator usługi do usunięcia</param>
		Task DeleteFavourAsync(int id);

        /// <summary>
        /// Pobiera wszystkie usługi związane z danym warsztatem.
        /// </summary>
        /// <param name="autoserviceId">Identyfikator warsztatu</param>
        /// <returns>Lista usług dostępnych w danym warsztacie</returns>
		Task<List<Favour>> GetFavoursByAutoServiceIdAsync(int autoserviceId);
	}
}
