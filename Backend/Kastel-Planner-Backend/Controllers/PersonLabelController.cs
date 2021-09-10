using Application.BeneficiaryWeeklyLogs.Requests;
using Microsoft.AspNetCore.Mvc;
using Application.BeneficiaryWeeklyLogs;
using System;
using System.Threading.Tasks;
using Application.Persons;
using Application.Persons.Requests;
using Application.PersonsLabels;
using Application.PersonsLabels.Requests;

namespace Kastel_Planner_Backend.Controllers
{
    public class PersonLabelController : Controller
    {
        private readonly IPersonsLabelsService _personsLabelsService;

        public PersonLabelController(IPersonsLabelsService personsLabelsService)
        {
            _personsLabelsService = personsLabelsService;
        }

        [Route("persons-labels")]
        public async Task<IActionResult> Index()
        {
            var personsLabels = await _personsLabelsService.GetAllPersonsLabelsAsync();
            return Ok(personsLabels);
        }

        [Route("persons-labels/{id}")]
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
        [Route("person-label")]
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


        [HttpDelete]
        [Route("person-label/{id}")]
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

        [HttpPatch]
        [Route("persons-labels/person-label/{id}")]
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
