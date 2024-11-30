using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Workshop.Core.Entities;
using Workshop.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Workshop.Infrastructure.Repositories;

namespace Workshop.Core.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IUserRepository _userRepository;
        private readonly string _jwtSecret;
        private readonly IPasswordHasherService _passwordHasherService;

        public RegisterService(IUserRepository userRepository, IConfiguration configuration, IPasswordHasherService passwordHasherService)
        {
            _userRepository = userRepository;
            _jwtSecret = configuration["Jwt:Secret"] ?? throw new ArgumentNullException(nameof(configuration), "Jwt:Secret is missing in the configuration.");
            _passwordHasherService = passwordHasherService;
        }

        public async Task<string> RegisterUserAsync(string login, string email, string password)
        {
            if (await _userRepository.EmailExistsAsync(email))
            {
                throw new Exception("Email is already in use.");
            }

            var user = new User
            {
                Login = login,
                Email = email,
                PasswordHash = _passwordHasherService.HashPassword(password)
            };

            await _userRepository.AddAsync(user);
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
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
