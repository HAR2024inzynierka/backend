using Workshop.Core.Entities;
using Workshop.Core.Interfaces;
using Workshop.Infrastructure.Repositories;

namespace Workshop.Core.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasherService _passwordHahserService;

        public RegisterService(IUserRepository userRepository, IPasswordHasherService passwordHasherService)
        {
            _userRepository = userRepository;
            _passwordHahserService = passwordHasherService;
        }

        public async Task<string> RegisterUserAsync(string login, string email, string password)
        {
            if(await _userRepository.EmailExistsAsync(email))
            {
                throw new Exception("Email is already in use.");
            }

            var user = new User
            {
                Login = login,
                Email = email,
                PasswordHash = _passwordHahserService.HashPassword(password)
            };

            await _userRepository.AddAsync(user);
            return "User registered successfully.";
        }
    }
}
