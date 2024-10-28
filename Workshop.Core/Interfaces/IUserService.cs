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
        public Task<User> GetUserByIdAsync(int userId);

        public Task AddVehicleAsync(Vehicle vehicle);
    }
}
