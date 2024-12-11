using Microsoft.AspNetCore.Mvc;
using Workshop.DTOs;
using Workshop.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Workshop.Core.Entities;

namespace Workshop.Controllers
{
    /// <summary>
    /// Kontroler admina odpowiedzialny za zarządzanie aplikacją.
    /// Wymaga autoryzacji użytkownika.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAutoRepairShopService _autoRepairShopService;
        private readonly IPostService _postService;

        /// <summary>
        /// Konstruktor kontrolera, który inicjalizuje zależności serwisów.
        /// </summary>
        /// <param name="adminService">Serwis do zarządzania użytkownikami.</param>
        /// <param name="autoRepairShopService">Serwis do zarządzania warsztatami.</param>
        /// <param name="postService">Serwis do zarządzania postami.</param>
        public AdminController(IUserService userService, IAutoRepairShopService autoRepairShopService, IPostService postService)
        {

            _userService = userService;
            _autoRepairShopService = autoRepairShopService;
            _postService = postService;
        }

        /// <summary>
        /// Pobiera wszystkich użytkowników z systemu.
        /// </summary>
        /// <returns>Lista użytkowników.</returns>
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Dodaje nowy warsztat do systemu.
        /// </summary>
        /// <param name="autoRepairShopDto">Dane warsztatu, który ma zostać dodany.</param>
        /// <returns>Status operacji.</returns>
		[HttpPost("workshop")]
        public async Task<IActionResult> AddAutoRepairShop([FromBody] AutoRepairShopDto autoRepairShopDto)
        {
            // Sprawdzamy, czy dane wejściowe są poprawne
            if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
                // Przygotowujemy obiekt AutoRepairShop z danymi
                var autoRepairShop = new AutoRepairShop
                {
                    Email = autoRepairShopDto.Email,
                    Address = autoRepairShopDto.Address,
                    PhoneNumber = autoRepairShopDto.PhoneNumber
                };

                await _autoRepairShopService.AddAutoRepairShopAsync(autoRepairShop);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

        /// <summary>
        /// Aktualizuje dane istniejącego warsztatu.
        /// </summary>
        /// <param name="autoServiceId">Identyfikator warsztatu, który ma zostać zaktualizowany.</param>
        /// <param name="autoServiceDto">Dane warsztatu do zaktualizowania.</param>
        /// <returns>Status operacji.</returns>
        [HttpPut("{autoServiceId}")]
        public async Task<IActionResult> UpdateAutoRepairShop(int autoServiceId, [FromBody] AutoRepairShopDto autoServiceDto)
        {
            // Sprawdzamy, czy dane wejściowe są poprawne
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Przygotowujemy obiekt AutoRepairShop z danymi do zaktualizowania.
                var autoRepairShop = new AutoRepairShop
                {
                    Id = autoServiceId,
                    Email = autoServiceDto.Email,
                    Address = autoServiceDto.Address,
                    PhoneNumber = autoServiceDto.PhoneNumber
                };

                await _autoRepairShopService.UpdateAsync(autoRepairShop);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Usuwa warsztat z systemu na podstawie jego identyfikatora.
        /// </summary>
        /// <param name="autoServiceId">Identyfikator warsztatu do usunięcia.</param>
        /// <returns>Status operacji.</returns>
        [HttpDelete("{autoServiceId}")]
        public async Task<IActionResult> DeleteAutoRepairShop(int autoServiceId)
        {
            try
            {
                await _autoRepairShopService.DeleteAsync(autoServiceId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("post")]
        public async Task<IActionResult> CreatePost([FromBody] PostDto postDto)
        {
            // Sprawdzamy poprawność danych wejściowych
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var post = new Post
                {
                    Title = postDto.Title,
                    Content = postDto.Content,
                    AutoRepairShopId = postDto.AutoRepairShopId,
                };

                await _postService.AddPostAsync(post);
                return Ok("Post added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpPut("post/{postId}")]
        public async Task<IActionResult> UpdatePost(int postId, [FromBody] PostDto postDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var post = new Post
                {
                    Id = postId,
                    Title = postDto.Title,
                    Content = postDto.Content,
                    AutoRepairShopId = postDto.AutoRepairShopId,
                };

                await _postService.UpdatePostAsync(post);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("post/{postId}")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            await _postService.DeletePostAsync(postId);
            return Ok();
        }
    }
}
