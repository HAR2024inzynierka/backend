using Microsoft.AspNetCore.Mvc;
using Workshop.Core.Entities;
using Workshop.DTOs;
using Workshop.Infrastructure.Repositories;
using Workshop.Core.Interfaces;

namespace Workshop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasherService _passwordHasherService;

        public RegisterController(IUserRepository userRepository, IPasswordHasherService passwordHasherService)
        {
            _userRepository = userRepository;
            _passwordHasherService = passwordHasherService;
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
                PasswordHash = _passwordHasherService.HashPassword(registerUserDto.Password)
            };

            await _userRepository.AddAsync(user);
            return Ok("User registered successfully.");
        }
    }
}
