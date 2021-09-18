using Application.BeneficiaryWeeklyLogs.Requests;
using Microsoft.AspNetCore.Mvc;
using Application.BeneficiaryWeeklyLogs;
using System;
using System.Threading.Tasks;
using Application.Persons;
using Application.Persons.Requests;

namespace Kastel_Planner_Backend.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [Route("persons")]
        public async Task<IActionResult> Index()
        {
            var persons = await _personService.GetAllPersonsAsync();
            return Ok(persons);
        }

        [Route("persons/{id}")]
        public async Task<IActionResult> Details([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _personService.GetPersonByAsync(id);
            if(result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost]
        [Route("person")]
        public async Task<IActionResult> Create([FromBody] CreatePersonRequest request)
        {
            if(request == null)
            {
                return BadRequest();
            }
            var result = await _personService.CreatePersonAsync(request);

            if(result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            Guid id1 = result.Value.Id;
            return CreatedAtAction(nameof(Details), new { id = result.Value.Id }, result.Value);
        }


        [HttpDelete]
        [Route("person/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _personService.DeletePersonAsync(id);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(null);
        }

        [HttpPatch]
        [Route("persons/person/{id}")]
        public async Task<IActionResult> Edit([FromRoute] Guid id, [FromBody] UpdatePersonRequest request)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _personService.UpdatePersonAsync(id, request);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

    }
}
