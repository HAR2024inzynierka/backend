using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using Workshop.Core.Entities;
using Workshop.DTOs;
using Workshop.Infrastructure.Repositories;

namespace Workshop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            if (await _userRepository.EmailExistsAsync(registerUserDto.Email))
            {
                return BadRequest("Email is already in use.");
            }

            var user = new User
            {
                Login = registerUserDto.Login,
                Email = registerUserDto.Email,
                PasswordHash = HashPassword(registerUserDto.Password)
            };

            await _userRepository.AddAsync(user);
            return Ok("User registered successfully.");
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

    }
}
