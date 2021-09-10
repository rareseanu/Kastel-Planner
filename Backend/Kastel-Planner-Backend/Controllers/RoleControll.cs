using Application.BeneficiaryWeeklyLogs.Requests;
using Microsoft.AspNetCore.Mvc;
using Application.BeneficiaryWeeklyLogs;
using System;
using System.Threading.Tasks;
using Application.Persons;
using Application.Persons.Requests;
using Application.PersonsLabels;
using Application.PersonsLabels.Requests;
using Application.PersonsRoles;
using Application.PersonsRoles.Requests;
using Application.Roles;
using Application.Roles.Requests;

namespace Kastel_Planner_Backend.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Route("roles")]
        public async Task<IActionResult> Index()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        [Route("roles/{id}")]
        public async Task<IActionResult> Details([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _roleService.GetRoleByAsync(id);
            if(result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost]
        [Route("role")]
        public async Task<IActionResult> Create([FromBody] CreateRoleRequest request)
        {
            if(request == null)
            {
                return BadRequest();
            }
            var result = await _roleService.CreateRoleAsync(request);

            if(result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(nameof(Details), new { id = result.Value.Id }, result.Value);
        }


        [HttpDelete]
        [Route("role/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _roleService.DeleteRoleAsync(id);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(null);
        }

        [HttpPatch]
        [Route("roles/role/{id}")]
        public async Task<IActionResult> Edit([FromRoute] Guid id, [FromBody] UpdateRoleRequest request)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _roleService.UpdateRoleAsync(id, request);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

    }
}
