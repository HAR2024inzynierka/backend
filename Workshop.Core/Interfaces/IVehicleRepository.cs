using Workshop.Core.Entities;


namespace Workshop.Infrastructure.Repositories
{
    public interface IVehicleRepository
    {
        Task AddAsync(Vehicle vehicle);
        Task<bool> VINExistsAsync(string registrationNumber);
        Task<List<Vehicle>> GetAllVehiclesOfUserAsync(int userId);
    }
}
