using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workshop.Core.Entities;
using Workshop.Core.Interfaces;
using Workshop.DTOs;

namespace Workshop.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/admin/[controller]")]
	public class TermController : ControllerBase
	{
		private readonly ITermService _termService;

		public TermController(ITermService termService)
		{
			_termService = termService;
		}

		[HttpGet("{termId}")]
		public async Task<IActionResult> GetTermById(int termId)
		{
            var term = await _termService.GetTermByIdAsync(termId);
            if (term == null)
            {
                return NotFound();
            }

            return Ok(term);
        }

		[HttpPost("add")]
		public async Task<IActionResult> AddTerm([FromBody] AddTermDto termDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				var term = new Term
				{
					StartDate = termDto.StartDate,
					EndDate = termDto.EndDate,
					Availability = termDto.Availability,
					AutoServiceId = termDto.AutoServiceId
				};

				await _termService.AddTermAsync(term);
				return Ok("Term added successfully.");
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

        [HttpPut("{termId}")]
        public async Task<IActionResult> UpdateTerm(int termId, [FromBody] AddTermDto termDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var term = new Term
                {
                    Id = termId,
                    StartDate = termDto.StartDate,
                    EndDate = termDto.EndDate,
                    Availability = termDto.Availability,
                    AutoServiceId = termDto.AutoServiceId

                };
                await _termService.UpdateTermAsync(term);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{termId}")]
        public async Task<IActionResult> DeleteTerm(int termId)
        {
            try
            {
                await _termService.DeleteTermAsync(termId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
