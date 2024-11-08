using Workshop.Core.Entities;
using Workshop.Infrastructure.Repositories;
using Workshop.Core.Interfaces;

namespace Workshop.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public UserService(IUserRepository userRepository, IVehicleRepository vehicleRepository)
        {
            _userRepository = userRepository;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }

        public async Task<List<Vehicle>> GetAllVehiclesAsync(int userId)
        {
            return await _vehicleRepository.GetAllVehiclesOfUserAsync(userId);
        }
        
        public async Task AddVehicleAsync(Vehicle vehicle)
        {
            Console.WriteLine($"Dodawanie pojazdu: {vehicle.Brand} {vehicle.Model} z numerem rejestracyjnym: {vehicle.RegistrationNumber} dla użytkownika: {vehicle.UserId}");

            if (await _vehicleRepository.VINExistsAsync(vehicle.VIN))
            {
                Console.WriteLine($"Błąd: Pojazd z VIN numerem {vehicle.VIN} już istnieje.");
                throw new Exception("A vehicle with the same VIN number already exists");
            }

            await _vehicleRepository.AddAsync(vehicle);
            Console.WriteLine($"Pojazd dodany pomyślnie: {vehicle.Id}");
        }

        public async Task UpdateVehicleAsync(Vehicle updateVehicle)
        {
            var vehicle = await _vehicleRepository.GetVehicleByIdAsync(updateVehicle.Id);
            if(vehicle == null)
            {
                throw new Exception("Vehicle not found");
            }

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
            if (vehicle == null)
            {
                throw new Exception("Vehicle not found");
            }
            await _vehicleRepository.DeleteAsync(vehicle);
        }

    }
}
