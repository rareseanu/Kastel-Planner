using Application.BeneficiaryWeeklyLogs.Requests;
using Microsoft.AspNetCore.Mvc;
using Application.BeneficiaryWeeklyLogs;
using System;
using System.Threading.Tasks;
using Application.PersonsRoles;
using Application.PersonsRoles.Requests;

namespace Kastel_Planner_Backend.Controllers
{
    [Route("api/[controller]")]
    public class PersonRoleController : Controller
    {
        private readonly IPersonsRolesService _personsRolesService;

        public PersonRoleController(IPersonsRolesService personsRolesService)
        {
            _personsRolesService = personsRolesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var personsRoles = await _personsRolesService.GetAllPersonsRolesAsync();
            return Ok(personsRoles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _personsRolesService.GetPersonRolesByAsync(id);
            if(result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePersonsRolesRequest request)
        {
            if(request == null)
            {
                return BadRequest();
            }
            var result = await _personsRolesService.CreatePersonRoleAsync(request);

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

            var result = await _personsRolesService.DeletePersonRoleAsync(id);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(null);
        }

        [HttpPost("removeRoles")]
        public async Task<IActionResult> RemoveRolesFromPerson([FromBody] RemoveRolesRequest request)
        {
            var result = await _personsRolesService.DeletePersonRolesAsync(request);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(null);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Edit([FromRoute] Guid id, [FromBody] UpdatePersonsRolesRequest request)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _personsRolesService.UpdatePersonRoleAsync(id, request);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

    }
}
