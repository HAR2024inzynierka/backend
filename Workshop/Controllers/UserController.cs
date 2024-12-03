using Microsoft.AspNetCore.Mvc;
using Workshop.Core.Interfaces;
using Workshop.Core.Entities;
using Workshop.DTOs;
using Microsoft.AspNetCore.Authorization;
using Workshop.Filters;
using Pomelo.EntityFrameworkCore.MySql.Storage.Internal;

namespace Workshop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [ServiceFilter(typeof(AuthorizeUserFilter))]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRecordService _recordService;

        public UserController(IUserService userService, IRecordService recordService)
        {
            _userService = userService;
            _recordService = recordService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("{userId}/vehicles")]
        public async Task<IActionResult> GetAllVehicles(int userId)
        {
            var vehicles = await _userService.GetAllVehiclesAsync(userId);
            return Ok(vehicles);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UpdateUserDto updateUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
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

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                await _userService.DeleteUserAsync(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("{userId}/vehicle")]
        public async Task<IActionResult> AddVehicle(int userId, [FromBody] VehicleDto vehicleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
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

        [HttpPut("{userId}/vehicle/{vehicleId}")]
        public async Task<IActionResult> UpdateVehicle(int vehicleId, [FromBody] VehicleDto vehicleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var vehicle = new Vehicle
                {
                    Id = vehicleId,
                    Brand = vehicleDto.Brand,
                    Model = vehicleDto.Model,
                    RegistrationNumber = vehicleDto.RegistrationNumber,
                    Capacity = vehicleDto.Capacity,
                    Power = vehicleDto.Power,
                    VIN = vehicleDto.VIN,
                    ProductionYear = vehicleDto.ProductionYear
                };
                await _userService.UpdateVehicleAsync(vehicle);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{userId}/vehicle/{vehicleId}")]
        public async Task<IActionResult> DeleteVehicle(int vehicleId)
        {
            try
            {
				await _userService.DeleteVehicleAsync(vehicleId);
				return Ok();
			}
            catch (Exception ex)
            {
				return BadRequest(new { message = ex.Message });
			}
            
        }

        [HttpGet("{userId}/records")]
        public async Task<IActionResult> GetUserRecords(int userId)
        {
            var records = await _recordService.GetRecordsByUserIdAsync(userId);
            return Ok(records);
        }

        [HttpDelete("{userId}/records/{recordId}")]
        public async Task<IActionResult> DeleteRecord(int recordId)
        {
            await _recordService.DeleteRecordAsync(recordId);
            return Ok();
        }
    }
}
