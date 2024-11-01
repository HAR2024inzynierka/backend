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

        /*public async Task AddVehicleAsync(int userId, string brand, string model, string registrationNumber)
        {
            if (await _vehicleRepository.RegistrationNumberExistsAsync(registrationNumber))
            {
                throw new Exception("A vehicle with the same registration number already exists");
            }

            var vehicle = new Vehicle
            {
                Brand = brand,
                Model = model,
                RegistrationNumber = registrationNumber,
                UserId = userId
            };

            await _vehicleRepository.AddAsync(vehicle);
        }*/
        
        public async Task AddVehicleAsync(int userId, string brand, string model, string registrationNumber)
        {
            Console.WriteLine($"Dodawanie pojazdu: {brand} {model} z numerem rejestracyjnym: {registrationNumber} dla użytkownika: {userId}");

            if (await _vehicleRepository.RegistrationNumberExistsAsync(registrationNumber))
            {
                Console.WriteLine($"Błąd: Pojazd z numerem rejestracyjnym {registrationNumber} już istnieje.");
                throw new Exception("A vehicle with the same registration number already exists");
            }

            var vehicle = new Vehicle
            {
                Brand = brand,
                Model = model,
                RegistrationNumber = registrationNumber,
                UserId = userId
            };

            await _vehicleRepository.AddAsync(vehicle);
            Console.WriteLine($"Pojazd dodany pomyślnie: {vehicle.Id}");
        }

    }
}
