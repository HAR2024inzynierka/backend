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

    }
}
