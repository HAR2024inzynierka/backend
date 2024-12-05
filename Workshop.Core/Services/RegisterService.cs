using Workshop.Core.Entities;
using Workshop.Core.Interfaces;
using Workshop.Infrastructure.Repositories;

namespace Workshop.Core.Services
{
    /// <summary>
    /// Serwis odpowiedzialny za rejestrację użytkowników.
    /// </summary>
    public class RegisterService : IRegisterService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGenerateJwtTokenService _generateJwtTokenService;
        private readonly IPasswordHasherService _passwordHasherService;

        /// <summary>
        /// Konstruktor serwisu, który przyjmuje zależności.
        /// </summary>
        /// <param name="userRepository">Repozytorium użytkowników, używane do dodawania nowych użytkowników i sprawdzania istniejących.</param>
        /// <param name="generateJwtTokenService">Serwis odpowiedzialny za generowanie tokenów.</param>
        /// <param name="passwordHasherService">Serwis odpowiedzialny za haszowanie haseł użytkowników.</param>
        public RegisterService(IUserRepository userRepository, IGenerateJwtTokenService generateJwtTokenService, IPasswordHasherService passwordHasherService)
        {
            _userRepository = userRepository;
            _generateJwtTokenService = generateJwtTokenService;
            _passwordHasherService = passwordHasherService;
        }

        public async Task<string> RegisterUserAsync(string login, string email, string password)
        {
            // Sprawdzanie, czy podany e-mail jest już używany.
            if (await _userRepository.EmailExistsAsync(email))
            {
                throw new Exception("Email is already in use.");
            }

            // Tworzenie nowego użytkownika z podanymi danymi.
            var user = new User
            {
                Login = login,
                Email = email,
                PasswordHash = _passwordHasherService.HashPassword(password)
            };

            await _userRepository.AddAsync(user);
            return _generateJwtTokenService.GenerateJwtToken(user);
        }
    }
}
