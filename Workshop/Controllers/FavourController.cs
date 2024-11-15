using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workshop.Core.Interfaces;
using Workshop.DTOs;
using Workshop.Core.Entities;
using Workshop.Core.Services;

namespace Workshop.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/admin/[controller]")]
	public class FavourController : ControllerBase
	{
		private readonly IFavourService _favourService;

		public FavourController(IFavourService favourService)
		{
			_favourService = favourService;
		}

		[HttpGet("{favourId}")]
		public async Task<IActionResult> GetFavourByIdAsync(int favourId)
		{
            var favour = await _favourService.GetFavourByIdAsync(favourId);
            if (favour == null)
            {
                return NotFound();
            }

            return Ok(favour);
        }

		[HttpPost("add")]
		public async Task<IActionResult> AddFavour([FromBody] AddFavourDto favourDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				var favour = new Favour
				{
					TypeName = favourDto.TypeName,
					Description = favourDto.Description,
					Price = favourDto.Price,
					AutoServiceId = favourDto.AutoServiceId
				};

				await _favourService.AddFavourAsync(favour);
				return Ok("Favour added successfully.");
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		[HttpPut("{favourId}")]
		public async Task<IActionResult> UpdateFavour(int favourId, [FromBody]AddFavourDto favourDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

            try
            {
                var favour = new Favour
                {
					Id = favourId,
                    TypeName = favourDto.TypeName,
					Description = favourDto.Description,
					Price = favourDto.Price,
                };
                await _favourService.UpdateFavourAsync(favour);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

		[HttpDelete("{favourId}")]
		public async Task<IActionResult> DeleteFavour(int favourId)
		{
            try
            {
                await _favourService.DeleteFavourAsync(favourId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
	}
}
