using Workshop.Core.Entities;

namespace Workshop.Core.Interfaces
{
    /// <summary>
    /// Interfejs dla serwisu Term, który odpowiada za operacje związane z terminami w systemie.
    /// </summary>
    public interface ITermService
	{
        /// <summary>
        /// Pobiera termin na podstawie jego identyfikatora.
        /// </summary>
        /// <param name="termId">Identyfikator terminu, którego szczegóły mają zostać pobrane.</param>
        /// <returns>Obiekt terminu o podanym identyfikatorze.</returns>
        Task<Term> GetTermByIdAsync(int termId);

        /// <summary>
        /// Dodaje nowy termin do systemu.
        /// </summary>
        /// <param name="term">Obiekt terminu do dodania.</param>
		Task AddTermAsync(Term term);

        /// <summary>
        /// Aktualizuje istniejący termin w systemie.
        /// </summary>
        /// <param name="term">Obiekt terminu z nowymi danymi, który ma zostać zaktualizowany.</param>
        Task UpdateTermAsync(Term term);

        /// <summary>
        /// Usuwa termin z systemu na podstawie jego identyfikatora.
        /// </summary>
        /// <param name="termId">Identyfikator terminu, który ma zostać usunięty.</param>
		Task DeleteTermAsync(int termId);

        /// <summary>
        /// Pobiera listę terminów związanych z danym warsztatem samochodowym.
        /// </summary>
        /// <param name="autoserviceId">Identyfikator warsztatu samochodowego, którego terminy mają zostać pobrane.</param>
        /// <returns>Lista terminów przypisanych do podanego warsztatu.</returns>
		Task<List<Term>> GetTermsByAutoServiceIdAsync(int autoserviceId);

        Task AddTermsForDayAsync(int autoServiceId, DateTime day);
    }
}
