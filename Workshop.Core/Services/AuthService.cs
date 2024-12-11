using System.Security.Claims;
using System.Text;
using Workshop.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Workshop.Core.Interfaces;
using Workshop.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Workshop.Core.Services
{
    /// <summary>
    /// Usługa odpowiedzialna za autoryzację użytkowników.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasherService _passwordHasherService;

        /// <summary>
        /// Konstruktor usługi autoryzacji.
        /// </summary>
        /// <param name="userRepository">Repozytorium użytkowników.</param>
        /// <param name="tokenService">Serwis odpowiedzialny za generowanie tokenów.</param>
        /// <param name="passwordHasherService">Usługa do weryfikacji haseł użytkowników.</param>
        public AuthService(IUserRepository userRepository, ITokenService tokenService, IPasswordHasherService passwordHasherService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _passwordHasherService = passwordHasherService;
        }

        public async Task<string> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            // Sprawdzenie, czy użytkownik istnieje oraz czy hasło jest poprawne
            if (
                user == null || 
                _passwordHasherService.VerifyHashedPassword(user.PasswordHash, password) != PasswordVerificationResult.Success
                )
            {
                throw new Exception("Invalid email or password");
            }

            return _tokenService.GenerateJwtToken(user);
        }
    }
}
