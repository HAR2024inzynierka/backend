using Microsoft.AspNetCore.Mvc;
using Workshop.Core.Interfaces;
using Workshop.Core.Entities;
using Workshop.DTOs;
using Microsoft.AspNetCore.Authorization;
using Workshop.Filters;

namespace Workshop.Controllers
{
    /// <summary>
    /// Kontroler odpowiedzialny za zarządzanie użytkownikami i ich pojazdami oraz rekordami warsztatu.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    //[ServiceFilter(typeof(AuthorizeUserFilter))]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRecordService _recordService;
        private readonly ITokenService _tokenService;

        /// <summary>
        /// Konstruktor kontrolera, który inicjalizuje serwis użytkowników i rekordów.
        /// </summary>
        /// <param name="userService">Serwis użytkowników.</param>
        /// <param name="recordService">Serwis rekordów.</param>
        /// <param name="tokenService">Serwis tokenów.</param>
        public UserController(IUserService userService, IRecordService recordService, ITokenService tokenService)
        {
            _userService = userService;
            _recordService = recordService;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Pobiera dane użytkownika na podstawie jego identyfikatora.
        /// </summary>
        /// <param name="userId">Identyfikator użytkownika.</param>
        /// <returns>Jeśli użytkownik istnieje, zwraca jego dane. W przeciwnym razie zwraca błąd 404.</returns>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);

            //Sprawdzamy czy termin istnieje. 
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        /// <summary>
        /// Pobiera wszystkie pojazdy przypisane do użytkownika.
        /// </summary>
        /// <returns>Lista pojazdów przypisanych do użytkownika.</returns>
        [HttpGet("vehicles")]
        public async Task<IActionResult> GetAllVehicles()
        {
            var userId = _tokenService.GetUserIdFromToken(HttpContext);
            var vehicles = await _userService.GetAllVehiclesAsync(userId);
            return Ok(vehicles);
        }

        /// <summary>
        /// Aktualizuje dane użytkownika.
        /// </summary>
        /// <param name="updateUserDto">Nowe dane użytkownika.</param>
        /// <returns>Status operacji (sukces lub błąd).</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto)
        {
            // Sprawdzanie poprawności danych wejściowych
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var userId = _tokenService.GetUserIdFromToken(HttpContext);
                
                // Tworzenie nowego obiektu User z danych DTO
                var user = new User
                {
                    Id = userId,
                    Login = updateUserDto.Login,
                    Email = updateUserDto.Email,
                    PhoneNumber = updateUserDto.PhoneNumber,
                };
                await _userService.UpdateUserAsync(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Usuwa użytkownika na podstawie jego identyfikatora.
        /// </summary>
        /// <returns>Status operacji (sukces lub błąd).</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteUser()
        {
            try
            {
                var userId = _tokenService.GetUserIdFromToken(HttpContext);
                await _userService.DeleteUserAsync(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Dodaje nowy pojazd do użytkownika.
        /// </summary>
        /// <param name="vehicleDto">Dane pojazdu do dodania.</param>
        /// <returns>Status operacji (sukces lub błąd).</returns>
        [HttpPost("vehicle")]
        public async Task<IActionResult> AddVehicle([FromBody] VehicleDto vehicleDto)
        {
            // Sprawdzanie poprawności danych wejściowych
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var userId = _tokenService.GetUserIdFromToken(HttpContext);

                // Tworzenie nowego obiektu Vehicle z danych DTO
                var vehicle = new Vehicle
                {
                    Brand = vehicleDto.Brand,
                    Model = vehicleDto.Model,
                    RegistrationNumber = vehicleDto.RegistrationNumber,
                    Capacity = vehicleDto.Capacity,
                    Power = vehicleDto.Power,
                    VIN = vehicleDto.VIN,
                    ProductionYear = vehicleDto.ProductionYear,
                    UserId = userId
                };
                await _userService.AddVehicleAsync(vehicle);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Aktualizuje dane pojazdu przypisanego do użytkownika.
        /// </summary>
        /// <param name="vehicleId">Identyfikator pojazdu.</param>
        /// <param name="vehicleDto">Nowe dane pojazdu.</param>
        /// <returns>Status operacji (sukces lub błąd).</returns>
        [HttpPut("vehicle/{vehicleId}")]
        public async Task<IActionResult> UpdateVehicle(int vehicleId, [FromBody] VehicleDto vehicleDto)
        {
            // Sprawdzanie poprawności danych wejściowych
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                // Tworzenie nowego obiektu Vehicle z danych DTO
                var vehicle = new Vehicle
                {
                    Id = vehicleId,
                    Brand = vehicleDto.Brand,
                    Model = vehicleDto.Model,
                    RegistrationNumber = vehicleDto.RegistrationNumber,
                    Capacity = vehicleDto.Capacity,
                    Power = vehicleDto.Power,
                    VIN = vehicleDto.VIN,
                    ProductionYear = vehicleDto.ProductionYear,
                    UserId = _tokenService.GetUserIdFromToken(HttpContext)
                };
                await _userService.UpdateVehicleAsync(vehicle);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Usuwa pojazd przypisany do użytkownika na podstawie jego identyfikatora.
        /// </summary>
        /// <param name="vehicleId">Identyfikator pojazdu do usunięcia.</param>
        /// <returns>Status operacji (sukces lub błąd).</returns>
        [HttpDelete("vehicle/{vehicleId}")]
        public async Task<IActionResult> DeleteVehicle(int vehicleId)
        {
            try
            {
                var userId = _tokenService.GetUserIdFromToken(HttpContext);
				await _userService.DeleteVehicleAsync(vehicleId, userId);
				return Ok();
			}
            catch (Exception ex)
            {
				return BadRequest(new { message = ex.Message });
			}
            
        }

        /// <summary>
        /// Pobiera rekordy warsztatowe przypisane do użytkownika.
        /// </summary>
        /// <returns>Lista rekordów warsztatowych przypisanych do użytkownika.</returns>
        [HttpGet("records")]
        public async Task<IActionResult> GetUserRecords()
        {
            var userId = _tokenService.GetUserIdFromToken(HttpContext);
            var records = await _recordService.GetRecordsByUserIdAsync(userId);
            return Ok(records);
        }

        /// <summary>
        /// Usuwa rekord warsztatowy na podstawie jego identyfikatora.
        /// </summary>
        /// <param name="recordId">Identyfikator rekordu do usunięcia.</param>
        /// <returns>Status operacji (sukces lub błąd).</returns>
        [HttpDelete("records/{recordId}")]
        public async Task<IActionResult> DeleteRecord(int recordId)
        {
            var userId = _tokenService.GetUserIdFromToken(HttpContext);
            await _recordService.DeleteRecordAsync(recordId, userId);
            return Ok();
        }


        //mozhno udalit esli poluchitsja vinesti getUserIdFromToken v pole kontrollera
        [HttpGet("user-id-from-token/{userId}")]
        public IActionResult GetUserIdFromToken()
        {
            try
            {
                var userIdfromToken = _tokenService.GetUserIdFromToken(HttpContext);
                return Ok(userIdfromToken);
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            
        }
    }
}
