using Application.TicketMessages;
using Application.TicketMessages.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Kastel_Planner_Backend.Controllers
{
    [Route("api/[controller]")]
    public class TicketMessageController : Controller
    {
        private readonly ITicketMessageService _ticketMessageService;

        public TicketMessageController(ITicketMessageService ticketMessageService)
        {
            _ticketMessageService = ticketMessageService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTicketMessageRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }
            var result = await _ticketMessageService.CreateTicketMessageAsync(request);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet("ticketmessages-by-id/{ticketMessageId}")]
        public async Task<IActionResult> GetSchedulesById([FromRoute] Guid ticketMessageId)
        {
            if (ticketMessageId == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _ticketMessageService.GetTicketMessagesByTicketIdAsync(ticketMessageId);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);

        }
    }
}
