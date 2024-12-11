using Workshop.Core.Entities;

namespace Workshop.Core.Interfaces
{
    /// <summary>
    /// Interfejs serwisu odpowiedzialnego za operacje związane z użytkownikami oraz ich pojazdami.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Pobiera użytkownika na podstawie jego identyfikatora.
        /// </summary>
        /// <param name="userId">Identyfikator użytkownika.</param>
        /// <returns>Obiekt użytkownika.</returns>
        Task<User> GetUserByIdAsync(int userId);

        /// <summary>
        /// Pobiera listę wszystkich użytkowników.
        /// </summary>
        /// <returns>Lista wszystkich użytkowników w systemie.</returns>
		Task<List<User>> GetAllUsersAsync();

        /// <summary>
        /// Pobiera wszystkie pojazdy przypisane do użytkownika.
        /// </summary>
        /// <param name="userId">Identyfikator użytkownika.</param>
        /// <returns>Lista pojazdów użytkownika.</returns>
        Task<List<Vehicle>> GetAllVehiclesAsync(int userId);

        /// <summary>
        /// Aktualizuje dane użytkownika.
        /// </summary>
        /// <param name="user">Obiekt użytkownika z nowymi danymi.</param>
        Task UpdateUserAsync(User user);

        /// <summary>
        /// Usuwa użytkownika na podstawie jego identyfikatora.
        /// </summary>
        /// <param name="userId">Identyfikator użytkownika, który ma zostać usunięty.</param>
        Task DeleteUserAsync(int userId);

        /// <summary>
        /// Dodaje nowy pojazd do systemu i przypisuje go do użytkownika.
        /// </summary>
        /// <param name="vehicle">Obiekt pojazdu, który ma zostać dodany.</param>
        Task AddVehicleAsync(Vehicle vehicle);

        /// <summary>
        /// Aktualizuje dane pojazdu przypisanego do użytkownika.
        /// </summary>
        /// <param name="vehicle">Obiekt pojazdu z nowymi danymi.</param>
        Task UpdateVehicleAsync(Vehicle vehicle);

        /// <summary>
        /// Usuwa pojazd przypisany do użytkownika na podstawie jego identyfikatora.
        /// </summary>
        /// <param name="vehicleId">Identyfikator pojazdu, który ma zostać usunięty.</param>
        /// <param name="userId">Identyfikator użytkownika, którego to pojazd.</param>
        Task DeleteVehicleAsync(int vehicleId, int userId);
    }
}
