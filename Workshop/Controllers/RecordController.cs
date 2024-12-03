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
    public class RecordController : ControllerBase
    {
        private readonly IRecordService _recordService;

        public RecordController(IRecordService recordService)
        {
            _recordService = recordService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUncompletedRecords()
        {
            var records = await _recordService.GetUncompletedRecordsAsync();
            return Ok(records);
        }

        [HttpPut("complete/{recordId}")]
        public async Task<IActionResult> CompleteRecord(int recordId)
        {
            await _recordService.CompleteRecordAsync(recordId);
            return Ok();
        }

    }
}
