using Workshop.Core.Entities;


namespace Workshop.Infrastructure.Repositories
{
    /// <summary>
    /// Interfejs repozytorium dla operacji związanych z pojazdami.
    /// Zawiera metody do operacji związane z pojazdami.
    /// </summary>
    public interface IVehicleRepository
    {
        /// <summary>
        /// Pobiera pojazd po jego identyfikatorze.
        /// </summary>
        /// <param name="id">Identyfikator pojazdu</param>
        /// <returns>Obiekt pojazdu, lub null, jeśli pojazd o podanym identyfikatorze nie istnieje</returns>
        Task<Vehicle?> GetVehicleByIdAsync(int id);

        /// <summary>
        /// Dodaje nowy pojazd do systemu.
        /// </summary>
        /// <param name="vehicle">Obiekt pojazdu, który ma zostać dodany</param>
        Task AddAsync(Vehicle vehicle);

        /// <summary>
        /// Aktualizuje dane istniejącego pojazdu w systemie.
        /// </summary>
        /// <param name="vehicle">Obiekt pojazdu z nowymi danymi</param>
        Task UpdateAsync(Vehicle vehicle);

        /// <summary>
        /// Usuwa pojazd z systemu.
        /// </summary>
        /// <param name="vehicle">Obiekt pojazdu do usunięcia</param>
        Task DeleteAsync(Vehicle vehicle);

        /// <summary>
        /// Sprawdza, czy pojazd o podanym numerze rejestracyjnym już istnieje w systemie.
        /// </summary>
        /// <param name="registrationNumber">Numer rejestracyjny pojazdu</param>
        /// <returns>True, jeśli pojazd z takim numerem rejestracyjnym istnieje, false w przeciwnym razie</returns>
        Task<bool> VINExistsAsync(string registrationNumber);

        /// <summary>
        /// Pobiera listę pojazdów przypisanych do określonego użytkownika.
        /// </summary>
        /// <param name="userId">Identyfikator użytkownika</param>
        /// <returns>Lista pojazdów przypisanych do użytkownika</returns>
        Task<List<Vehicle>> GetAllVehiclesOfUserAsync(int userId);
    }
}
