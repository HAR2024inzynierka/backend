using Workshop.Core.Entities;


namespace Workshop.Infrastructure.Repositories
{
    public interface IVehicleRepository
    {
        Task AddAsync(Vehicle vehicle);
    }
}
