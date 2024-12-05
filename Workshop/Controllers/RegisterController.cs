using Microsoft.AspNetCore.Mvc;
using Workshop.DTOs;
using Workshop.Core.Interfaces;

namespace Workshop.Controllers
{
    /// <summary>
    /// Kontroler odpowiedzialny za rejestrację użytkowników w systemie.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _registerService;

        /// <summary>
        /// Konstruktor kontrolera, który inicjalizuje serwis do rejestracji użytkowników.
        /// </summary>
        /// <param name="registerService">Serwis odpowiedzialny za rejestrację użytkowników.</param>
        public RegisterController(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        /// <summary>
        /// Rejestruje nowego użytkownika w systemie.
        /// </summary>
        /// <param name="registerUserDto">Dane użytkownika do rejestracji.</param>
        /// <returns>Token uwierzytelniający użytkownika, jeśli rejestracja przebiegła pomyślnie.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            // Sprawdzenie, czy dane wejściowe są poprawne
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var token = await _registerService.RegisterUserAsync(registerUserDto.Login, registerUserDto.Email, registerUserDto.Password);
                return Ok(new {token});
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
