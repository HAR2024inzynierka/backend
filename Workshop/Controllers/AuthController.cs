using Microsoft.AspNetCore.Mvc;
using Workshop.Core.Interfaces;
using Workshop.DTOs;

namespace Workshop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUserDto loginUserDto)
        {
            var token = _authService.Authenticate(loginUserDto.Email, loginUserDto.Password);

            if(token == null)
            {
                return Unauthorized(new {message = "Invalid email or password."});
            }

            return Ok(new {token});
        }
    }
}   
