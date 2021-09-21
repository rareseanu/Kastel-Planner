using Application.Schedules;
using Application.Schedules.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Kastel_Planner_Backend.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [Route("schedules")]
        public async Task<IActionResult> Index([FromQuery] GetSchedulesRequest request)
        {
            
            var schedules = await _scheduleService.GetAllSchedulesAsync(request);
            return Ok(schedules);
        }

        [Route("schedules/{id}")]
        public async Task<IActionResult> Details([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _scheduleService.GetScheduleByAsync(id);
            if(result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost]
        [Route("schedule")]
        public async Task<IActionResult> Create([FromBody] CreateScheduleRequest request)
        {
            if(request == null)
            {
                return BadRequest();
            }
            var result = await _scheduleService.CreateScheduleAsync(request);
            if(result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(nameof(Details), new { id = result.Value.Id}, result.Value);
        }

    

        [HttpDelete]
        [Route("schedule/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _scheduleService.DeleteScheduleAsync(id);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(null);
        }

        [HttpPatch]
        [Route("schedules/schedule/{id}")]
        public async Task<IActionResult> Edit([FromRoute] Guid id, [FromBody] UpdateScheduleRequest request)
        {
            if(id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _scheduleService.UpdateScheduleAsync(id, request);
            if(result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

    }
}
