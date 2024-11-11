using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Workshop.Core.Entities;
using Workshop.Core.Interfaces;
using Workshop.Core.Services;
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

	}
}
