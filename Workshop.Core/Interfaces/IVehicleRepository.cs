using Workshop.Core.Entities;


namespace Workshop.Infrastructure.Repositories
{
    public interface IVehicleRepository
    {
        Task<Vehicle?> GetVehicleByIdAsync(int id);
        Task AddAsync(Vehicle vehicle);
        Task UpdateAsync(Vehicle vehicle);
        Task DeleteAsync(Vehicle vehicle);
        Task<bool> VINExistsAsync(string registrationNumber);
        Task<List<Vehicle>> GetAllVehiclesOfUserAsync(int userId);
    }
}
