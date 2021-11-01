using Application.Tickets;
using Application.Tickets.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Kastel_Planner_Backend.Controllers
{
    [Route("api/[controller]")]
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tickets = await _ticketService.GetAllTicketsAsync();
            return Ok(tickets);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTicketRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }
            var result = await _ticketService.CreateTicketAsync(request);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet("ticket-by-id/{userId}")]
        public async Task<IActionResult> GetSchedulesById([FromRoute] Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _ticketService.GetTicketsByUserIdAsync(userId);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);

        }
    }
}
