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
        private readonly string _jwtSecret; // Sekret używany do generowania tokenów JWT.
        private readonly IPasswordHasherService _passwordHasherService;

        /// <summary>
        /// Konstruktor usługi autoryzacji.
        /// </summary>
        /// <param name="userRepository">Repozytorium użytkowników.</param>
        /// <param name="configuration">Konfiguracja aplikacji, z której pobierany jest sekret JWT.</param>
        /// <param name="passwordHasherService">Usługa do weryfikacji haseł użytkowników.</param>
        public AuthService(IUserRepository userRepository, IConfiguration configuration, IPasswordHasherService passwordHasherService)
        {
            _userRepository = userRepository;
            _jwtSecret = configuration["Jwt:Secret"] ?? throw new ArgumentNullException(nameof(configuration), "Jwt:Secret is missing in the configuration.");
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

            return GenerateJwtToken(user);
        }

        /// <summary>
        /// Generowanie tokenu JWT na podstawie danych użytkownika.
        /// </summary>
        /// <param name="user">Obiekt użytkownika, dla którego generowany jest token.</param>
        /// <returns>Token JWT w postaci ciągu znaków.</returns>
        public string GenerateJwtToken(User user) // wynesti metod v otdelnij servic i iz registerService tozhe
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Dodanie informacji o użytkowniku (Claims)
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1), // Token jest ważny przez 1 dzień

                // Użycie HMAC SHA-256 do podpisania tokenu
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token); // Zwrócenie tokenu jako ciągu znaków
        }
    }
}
