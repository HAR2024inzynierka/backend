using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Workshop.Core.Entities;
using Workshop.Core.Interfaces;

namespace Workshop.Core.Services
{
    /// <summary>
    /// Serwis odpowiedzialny za generowanie JWT tokena.
    /// </summary>
    public class GenerateJwtTokenService : IGenerateJwtTokenService
    {
        private readonly string _jwtSecret; // Sekret używany do generowania tokenów JWT.

        /// <summary>
        /// Konstruktor usługi Generowania Tokena.
        /// </summary>
        /// <param name="configuration">Konfiguracja aplikacji, z której pobierany jest sekret JWT.</param>
        public GenerateJwtTokenService(IConfiguration configuration)
        {
            _jwtSecret = configuration["Jwt:Secret"] ?? throw new ArgumentNullException(nameof(configuration), "Jwt:Secret is missing in the configuration.");
        }

        public string GenerateJwtToken(User user)
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
