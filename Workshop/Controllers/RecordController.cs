using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workshop.Core.Interfaces;

namespace Workshop.Controllers
{
    /// <summary>
    /// Kontroler odpowiedzialny za operacje związane z zapisami w warsztatach.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/admin/[controller]")]
    public class RecordController : ControllerBase
    {
        private readonly IRecordService _recordService;

        /// <summary>
        /// Konstruktor kontrolera, który inicjalizuje serwis do obsługi zapisów.
        /// </summary>
        /// <param name="recordService">Serwis odpowiedzialny za operacje na rekordach.</param>
        public RecordController(IRecordService recordService)
        {
            _recordService = recordService;
        }

        /// <summary>
        /// Pobiera listę niezakończonych zapisów.
        /// </summary>
        /// <returns>Lista niezakończonych zapisów.</returns>
        [HttpGet]
        public async Task<IActionResult> GetUncompletedRecords()
        {
            var records = await _recordService.GetUncompletedRecordsAsync();
            return Ok(records);
        }

        /// <summary>
        /// Zmienia status zapisu na zakończony.
        /// </summary>
        /// <param name="recordId">Identyfikator zapisu, który ma zostać zakończony.</param>
        /// <returns>Status operacji zakończenia zapisu.</returns>
        [HttpPut("complete/{recordId}")]
        public async Task<IActionResult> CompleteRecord(int recordId)
        {
            await _recordService.CompleteRecordAsync(recordId);
            return Ok();
        }

    }
}
