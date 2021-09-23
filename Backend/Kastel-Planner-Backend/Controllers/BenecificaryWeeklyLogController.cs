using Application.BeneficiaryWeeklyLogs.Requests;
using Microsoft.AspNetCore.Mvc;
using Application.BeneficiaryWeeklyLogs;
using System;
using System.Threading.Tasks;

namespace Kastel_Planner_Backend.Controllers
{
    public class BenecificaryWeeklyLogController : Controller
    {
        private readonly IBeneficiaryWeeklyLogService _weeklyService;

        public BenecificaryWeeklyLogController(IBeneficiaryWeeklyLogService weeklyService)
        {
            _weeklyService = weeklyService;
        }

        [Route("weeklylogs")]
        public async Task<IActionResult> Index()
        {
            var weeklyLogs = await _weeklyService.GetAllWeeklyLogsAsync();
            return Ok(weeklyLogs);
        }

        [Route("weekly-logs-by-id/{personId}")]
        public async Task<IActionResult> GetWeeklyLogsByPersonId([FromRoute] Guid personId)
        {
            if (personId == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _weeklyService.GetAllWeeklyLogsByPersonId(personId);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);

        }

        [Route("weeklylogs/{id}")]
        public async Task<IActionResult> Details([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _weeklyService.GetWeeklyLogByAsync(id);
            if(result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost]
        [Route("weeklylog")]
        public async Task<IActionResult> Create([FromBody] CreateBeneficiaryWeeklyLogRequest request)
        {
            if(request == null)
            {
                return BadRequest();
            }
            var result = await _weeklyService.CreateWeeklyLogAsync(request);

            if(result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(nameof(Details), new { id = result.Value.Id }, result.Value);
        }


        [HttpDelete]
        [Route("weeklylog/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _weeklyService.DeleteWeeklyLogAsync(id);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(null);
        }

        [HttpPatch]
        [Route("weeklylogs/weeklylog/{id}")]
        public async Task<IActionResult> Edit([FromRoute] Guid id, [FromBody] UpdateBeneficiaryWeeklyLog request)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _weeklyService.UpdateWeeklyLogAsync(id, request);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

    }
}
