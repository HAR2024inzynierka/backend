using Workshop.Core.Entities;

namespace Workshop.Infrastructure.Repositories
{
    /// <summary>
    /// Interfejs repozytorium do zarządzania użytkownikami w systemie warsztatów samochodowych.
    /// Określa operacje na danych związanych z użytkownikami.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Pobiera użytkownika na podstawie jego identyfikatora.
        /// </summary>
        /// <param name="id">Identyfikator użytkownika</param>
        /// <returns>Obiekt użytkownika, lub null, jeśli nie znaleziono</returns>
        Task<User?> GetByIdAsync(int id);

        /// <summary>
        /// Pobiera użytkownika na podstawie jego adresu e-mail.
        /// </summary>
        /// <param name="email">Adres e-mail użytkownika</param>
        /// <returns>Obiekt użytkownika, lub null, jeśli nie znaleziono</returns>
        Task<User?> GetByEmailAsync(string email);

        /// <summary>
        /// Dodaje nowego użytkownika do systemu.
        /// </summary>
        /// <param name="user">Obiekt użytkownika do dodania</param>
        Task AddAsync(User user);

        /// <summary>
        /// Aktualizuje dane istniejącego użytkownika.
        /// </summary>
        /// <param name="user">Obiekt użytkownika z nowymi danymi</param>
        Task UpdateAsync(User uesr);

        /// <summary>
        /// Usuwa użytkownika z systemu.
        /// </summary>
        /// <param name="user">Obiekt użytkownika do usunięcia</param>
        Task DeleteAsync(User user);

        /// <summary>
        /// Sprawdza, czy użytkownik o podanym adresie e-mail istnieje w systemie.
        /// </summary>
        /// <param name="email">Adres e-mail użytkownika</param>
        /// <returns>True, jeśli użytkownik z takim adresem e-mail istnieje, false w przeciwnym razie</returns>
        Task<bool> EmailExistsAsync(string email);

        /// <summary>
        /// Pobiera listę wszystkich użytkowników w systemie.
        /// </summary>
        /// <returns>Lista użytkowników</returns>
        Task<List<User>> GetAllUsersAsync();
    }
}
