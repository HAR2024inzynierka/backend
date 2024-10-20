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
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var token = await _authService.AuthenticateAsync(loginUserDto.Email, loginUserDto.Password);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}   
