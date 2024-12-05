using Workshop.Core.Entities;
using Workshop.Infrastructure.Repositories;
using Workshop.Core.Interfaces;

namespace Workshop.Core.Services
{
    /// <summary>
    /// Serwis odpowiedzialny za operacje związane z użytkownikami oraz ich pojazdami.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IVehicleRepository _vehicleRepository;

        /// <summary>
        /// Konstruktor serwisu UserService.
        /// </summary>
        /// <param name="userRepository">Repozytorium operujące na danych użytkowników</param>
        /// <param name="vehicleRepository">Repozytorium operujące na danych pojazdów</param>
        public UserService(IUserRepository userRepository, IVehicleRepository vehicleRepository)
        {
            _userRepository = userRepository;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            // Jeśli użytkownik nie istnieje, rzucamy wyjątek
            if (user == null)
            {
                throw new Exception("User not found");
            }

            return user;
        }

        public async Task<List<Vehicle>> GetAllVehiclesAsync(int userId)
        {
            return await _vehicleRepository.GetAllVehiclesOfUserAsync(userId);
        }

        public async Task UpdateUserAsync(User updateUser)
        {
            var user = await _userRepository.GetByIdAsync(updateUser.Id);

            // Jeśli użytkownik nie istnieje, rzucamy wyjątek
            if (user == null)
            {
                throw new Exception("User not found");
            }

            // Aktualizowanie pól użytkownika
            user.Login = updateUser.Login;
            user.Email = updateUser.Email;
            user.PhoneNumber = updateUser.PhoneNumber;

            await _userRepository.UpdateAsync(user);
        }
        public async Task DeleteUserAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            // Jeśli użytkownik nie istnieje, rzucamy wyjątek
            if (user == null)
            {
                throw new Exception("User not found");
            }

            await _userRepository.DeleteAsync(user);
        }
        
        public async Task AddVehicleAsync(Vehicle vehicle)
        {
            // Sprawdza, czy pojazd o takim samym numerze VIN już istnieje
            if (await _vehicleRepository.VINExistsAsync(vehicle.VIN))
            {
                throw new Exception("A vehicle with the same VIN number already exists");
            }

            await _vehicleRepository.AddAsync(vehicle);
        }

        public async Task UpdateVehicleAsync(Vehicle updateVehicle)
        {
            var vehicle = await _vehicleRepository.GetVehicleByIdAsync(updateVehicle.Id);

            // Jeśli pojazd nie istnieje, rzucamy wyjątek
            if (vehicle == null)
            {
                throw new Exception("Vehicle not found");
            }

            // Aktualizuje dane pojazdu
            vehicle.Brand = updateVehicle.Brand;
            vehicle.Model = updateVehicle.Model;
            vehicle.RegistrationNumber = updateVehicle.RegistrationNumber;
            vehicle.Capacity = updateVehicle.Capacity;
            vehicle.Power = updateVehicle.Power;
            vehicle.VIN = updateVehicle.VIN;
            vehicle.ProductionYear = updateVehicle.ProductionYear;

            await _vehicleRepository.UpdateAsync(vehicle);
        }

        public async Task DeleteVehicleAsync(int vehicleId)
        {
            var vehicle  = await _vehicleRepository.GetVehicleByIdAsync(vehicleId);

            // Jeśli pojazd nie istnieje, rzucamy wyjątek
            if (vehicle == null)
            {
                throw new Exception("Vehicle not found");
            }
            await _vehicleRepository.DeleteAsync(vehicle);
        }

    }
}
