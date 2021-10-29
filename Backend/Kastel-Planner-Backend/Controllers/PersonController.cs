using Application.BeneficiaryWeeklyLogs.Requests;
using Microsoft.AspNetCore.Mvc;
using Application.BeneficiaryWeeklyLogs;
using System;
using System.Threading.Tasks;
using Application.Persons;
using Application.Persons.Requests;

namespace Kastel_Planner_Backend.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var persons = await _personService.GetAllPersonsAsync();
            return Ok(persons);
        }

        [HttpGet("{id}")]
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


        [HttpDelete("{id}")]
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

        [HttpPatch("{id}")]
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
