using Workshop.Core.Entities;

namespace Workshop.Core.Interfaces
{
    /// <summary>
    /// Interfejs repozytorium dla rekordów w systemie warsztatów.
    /// Zawiera operacje do pobierania, dodawania, aktualizowania oraz usuwania rekordów.
    /// </summary>
    public interface IRecordRepository
	{
        /// <summary>
        /// Pobiera rekord na podstawie jego identyfikatora.
        /// </summary>
        /// <param name="id">Identyfikator rekordu.</param>
        /// <returns>Rekord o wskazanym identyfikatorze lub null, jeśli nie znaleziono.</returns>
        Task<Record?> GetRecordByIdAsync(int id);

        /// <summary>
        /// Dodaje nowy rekord do bazy danych.
        /// </summary>
        /// <param name="record">Rekord do dodania.</param>
		Task AddRecordAsync(Record record);

        /// <summary>
        /// Aktualizuje istniejący rekord w bazie danych.
        /// </summary>
        /// <param name="record">Rekord z nowymi danymi.</param>
		Task UpdateRecordAsync(Record record);

        /// <summary>
        /// Usuwa rekord z bazy danych.
        /// </summary>
        /// <param name="record">Rekord do usunięcia.</param>
        Task DeleteRecordAsync(Record record);

        /// <summary>
        /// Pobiera wszystkie rekordy związane z użytkownikiem na podstawie jego identyfikatora.
        /// </summary>
        /// <param name="userId">Identyfikator użytkownika.</param>
        /// <returns>Lista rekordów przypisanych do użytkownika.</returns>
		Task<List<Record>> GetRecordsByUserIdAsync(int userId);

        /// <summary>
        /// Pobiera wszystkie rekordy, które nie zostały jeszcze ukończone.
        /// </summary>
        /// <returns>Lista nieukończonych rekordów.</returns>
		Task<List<Record>> GetUncompletedRecordsAsync();
	}
}
