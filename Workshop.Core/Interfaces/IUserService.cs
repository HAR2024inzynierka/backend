using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop.Core.Entities;
using Workshop.Infrastructure.Repositories;

namespace Workshop.Core.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int userId);

        Task<List<Vehicle>> GetAllVehiclesAsync(int userId);

        Task UpdateUserAsync(User user);

        Task DeleteUserAsync(int userId);

        Task AddVehicleAsync(Vehicle vehicle);

        Task UpdateVehicleAsync(Vehicle vehicle);

        Task DeleteVehicleAsync(int vehicleId);
    }
}
