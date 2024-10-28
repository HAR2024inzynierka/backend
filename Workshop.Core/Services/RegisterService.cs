using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Workshop.Core.Entities;
using Workshop.Core.Interfaces;
using Workshop.Infrastructure.Repositories;

namespace Workshop.Core.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IUserRepository _userRepository;
        private readonly string _jwtSecret;
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
