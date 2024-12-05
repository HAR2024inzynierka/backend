using Workshop.Core.Entities;

namespace Workshop.Core.Interfaces
{
    /// <summary>
    /// Interfejs odpowiedzialny za operacje na rekordach wizyt w warsztacie.
    /// </summary>
    public interface IRecordService
	{
        /// <summary>
        /// Pobiera rekord wizyty na podstawie jego identyfikatora.
        /// </summary>
        /// <param name="id">Identyfikator rekordu wizyty</param>
        /// <returns>Rekord wizyty</returns>
        Task<Record> GetRecordByIdAsync(int id);

        /// <summary>
        /// Dodaje nowy rekord wizyty do bazy danych.
        /// </summary>
        /// <param name="record">Obiekt rekordu wizyty, który ma zostać dodany</param>
        Task AddRecordAsync(Record record);

        /// <summary>
        /// Aktualizuje istniejący rekord wizyty.
        /// </summary>
        /// <param name="record">Obiekt rekordu wizyty z nowymi danymi</param>
        Task UpdateRecordAsync(Record record);

        /// <summary>
        /// Usuwa rekord wizyty na podstawie jego identyfikatora.
        /// </summary>
        /// <param name="id">Identyfikator rekordu wizyty, który ma zostać usunięty</param>
        Task DeleteRecordAsync(int id);

        /// <summary>
        /// Pobiera listę rekordów wizyt powiązanych z użytkownikiem na podstawie jego identyfikatora.
        /// </summary>
        /// <param name="userId">Identyfikator użytkownika, którego rekordy mają zostać pobrane</param>
        /// <returns>Lista rekordów wizyt powiązanych z użytkownikiem</returns>
		Task<List<Record>> GetRecordsByUserIdAsync(int userId);

        /// <summary>
        /// Pobiera listę nieukończonych rekordów wizyt.
        /// </summary>
        /// <returns>Lista nieukończonych rekordów wizyt</returns>
        Task<List<Record>> GetUncompletedRecordsAsync();

        /// <summary>
        /// Oznacza rekord wizyty jako ukończony na podstawie jego identyfikatora.
        /// </summary>
        /// <param name="recordId">Identyfikator rekordu wizyty, który ma zostać oznaczony jako ukończony</param>
        Task CompleteRecordAsync(int recordId);
    }
}
