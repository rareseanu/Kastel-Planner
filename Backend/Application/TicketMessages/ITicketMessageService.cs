using Application.TicketMessages.Requests;
using Application.TicketMessages.Responses;
using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.TicketMessages
{
    public interface ITicketMessageService
    {
        Task<Result<IList<TicketMessageResponse>>> GetTicketMessagesByTicketIdAsync(Guid ticketId);
        Task<Result<TicketMessageResponse>> CreateTicketMessageAsync(CreateTicketMessageRequest request);
    }
}
