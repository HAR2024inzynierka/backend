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
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly string _jwtSecret;
        private readonly IPasswordHasherService _passwordHasherService;

        public AuthService(IUserRepository userRepository, IConfiguration configuration, IPasswordHasherService passwordHasherService)
        {
            _userRepository = userRepository;
            _jwtSecret = configuration["Jwt:Secret"];
            _passwordHasherService = passwordHasherService;
        }

        public async Task<string> Authenticate(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (
                user == null || 
                _passwordHasherService.VerifyHashedPassword(user.PasswordHash, password) != PasswordVerificationResult.Success
                )
            {
                return null;
            }

            return GenerateJwtToken(user);
        }

        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
