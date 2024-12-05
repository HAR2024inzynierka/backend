using Microsoft.AspNetCore.Mvc;
using Workshop.Core.Interfaces;
using Workshop.DTOs;

namespace Workshop.Controllers
{
    /// <summary>
    /// Kontroler odpowiedzialny za autentykację użytkowników.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Konstruktor kontrolera, który inicjalizuje zależność serwisu autentykacji.
        /// </summary>
        /// <param name="authService">Serwis odpowiedzialny za autentykację użytkowników.</param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Logowanie użytkownika przy użyciu adresu e-mail i hasła.
        /// </summary>
        /// <param name="loginUserDto">Dane użytkownika do logowania.</param>
        /// <returns>Token JWT w odpowiedzi, jeśli logowanie się powiedzie.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            // Sprawdzamy, czy dane wejściowe są poprawne
            if (!ModelState.IsValid)
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
