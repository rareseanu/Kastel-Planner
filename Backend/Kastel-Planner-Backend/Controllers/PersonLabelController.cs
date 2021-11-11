using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Application.PersonsLabels;
using Application.PersonsLabels.Requests;

namespace Kastel_Planner_Backend.Controllers
{
    [Route("api/[controller]")]
    public class PersonLabelController : Controller
    {
        private readonly IPersonsLabelsService _personsLabelsService;

        public PersonLabelController(IPersonsLabelsService personsLabelsService)
        {
            _personsLabelsService = personsLabelsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var personsLabels = await _personsLabelsService.GetAllPersonsLabelsAsync();
            return Ok(personsLabels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _personsLabelsService.GetPersonLabelByAsync(id);
            if(result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePersonsLabelsRequest request)
        {
            if(request == null)
            {
                return BadRequest();
            }
            var result = await _personsLabelsService.CreatePersonLabelAsync(request);

            if(result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(nameof(Details), new { id = result.Value.Id }, result.Value);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _personsLabelsService.DeletePersonLabelAsync(id);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(null);
        }

        [HttpPost("removeLabels")]
        public async Task<IActionResult> RemoveLabelsFromPerson([FromBody] RemoveLabelsRequest request)
        {
            var result = await _personsLabelsService.DeletePersonLabels(request);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(null);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Edit([FromRoute] Guid id, [FromBody] UpdatePersonsLabelsRequest request)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _personsLabelsService.UpdatePersonLabelAsync(id, request);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

    }
}
