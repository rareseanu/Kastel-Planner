using Application.Labels;
using Application.Labels.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Kastel_Planner_Backend.Controllers
{
    [Route("api/[controller]")]
    public class LabelController : Controller
    {
        private readonly ILabelService _labelService;

        public LabelController(ILabelService labelService)
        {
            _labelService = labelService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var labels = await _labelService.GetAllLabelsAsync();
            return Ok(labels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _labelService.GetLabelByAsync(id);
            if(result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLabelRequest request)
        {
            if(request == null)
            {
                return BadRequest();
            }
            var result = await _labelService.CreateLabelAsync(request);
            if(result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(nameof(Details), new { id = result.Value.Id}, result.Value);
        }

    

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _labelService.DeleteLabelAsync(id);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(null);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Edit([FromRoute] Guid id, [FromBody] UpdateLabelRequest request)
        {
            if(id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _labelService.UpdateLabelAsync(id, request);
            if(result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

    }
}
