using Workshop.Core.Entities;

namespace Workshop.Core.Interfaces
{
    /// <summary>
    /// Interfejs repozytorium do zarządzania terminami wizyt w systemie warsztatów samochodowych.
    /// Określa operacje na danych związanych z terminami wizyt.
    /// </summary>
    public interface ITermRepository
	{
        /// <summary>
        /// Pobiera termin wizyty na podstawie jego identyfikatora.
        /// </summary>
        /// <param name="termId">Identyfikator terminu wizyty.</param>
        /// <returns>Termin wizyty o wskazanym identyfikatorze lub null, jeśli nie znaleziono.</returns>
        Task<Term?> GetTermByIdAsync(int termId);

        /// <summary>
        /// Dodaje nowy termin wizyty do systemu.
        /// </summary>
        /// <param name="term">Obiekt reprezentujący termin wizyty.</param>
        Task AddTermAsync(Term term);

        /// <summary>
        /// Aktualizuje istniejący termin wizyty w systemie.
        /// </summary>
        /// <param name="term">Zaktualizowany obiekt terminu wizyty.</param>
		Task UpdateTermAsync(Term term);

        /// <summary>
        /// Usuwa termin wizyty z systemu.
        /// </summary>
        /// <param name="term">Termin wizyty do usunięcia.</param>
		Task DeleteTermAsync(Term term);

        /// <summary>
        /// Pobiera listę terminów wizyt przypisanych do określonego warsztatu.
        /// </summary>
        /// <param name="autoserviceId">Identyfikator warsztatu, dla którego pobieramy terminy.</param>
        /// <returns>Lista terminów przypisanych do warsztatu.</returns>
		Task<List<Term>> GetTermsByAutoServiceIdAsync(int autoserviceId);
	}
}
