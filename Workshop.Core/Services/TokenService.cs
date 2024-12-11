using Microsoft.AspNetCore.Http;
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
    public class TokenService : ITokenService
    {
        private readonly string _jwtSecret; // Sekret używany do generowania tokenów JWT.

        /// <summary>
        /// Konstruktor usługi Generowania Tokena.
        /// </summary>
        /// <param name="configuration">Konfiguracja aplikacji, z której pobierany jest sekret JWT.</param>
        public TokenService(IConfiguration configuration)
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

        public int GetUserIdFromToken(HttpContext httpContext)
        {
            var authHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (authHeader == null || !authHeader.StartsWith("Bearer "))
            {
                throw new Exception("Token not found.");
            }

            var token = authHeader.Substring("Bearer ".Length);

            var handler = new JwtSecurityTokenHandler();

            if (!handler.CanReadToken(token))
            {
                throw new Exception("Incorrect Token.");
            }

            var jwtToken = handler.ReadJwtToken(token);

            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;


            if(userIdClaim == null)
            {
                throw new Exception("User ID not found in token.");
            }

            if (!int.TryParse(userIdClaim, out int userId))
            {
                throw new Exception("Invalid User ID format in token.");
            }

            return userId;
        }
    }
}
