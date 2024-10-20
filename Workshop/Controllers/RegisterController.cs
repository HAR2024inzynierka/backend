using Microsoft.AspNetCore.Mvc;
using Workshop.DTOs;
using Workshop.Core.Interfaces;

namespace Workshop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _registerService;

        public RegisterController(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _registerService.RegisterUserAsync(registerUserDto.Login, registerUserDto.Email, registerUserDto.Password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
