using Application.Tickets.Requests;
using Application.Tickets.Responses;
using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Tickets
{
    public interface ITicketService
    {
        Task<IList<TicketResponse>> GetAllTicketsAsync();
        Task<Result<TicketResponse>> GetTicketByIdAsync(Guid id);
        Task<Result<IList<TicketResponse>>> GetTicketsByUserIdAsync(Guid userId);
        Task<Result<TicketResponse>> CreateTicketAsync(CreateTicketRequest request);
        Task<Result<TicketResponse>> UpdateTicketAsync(Guid ticketID, UpdateTicketRequest request);
        Task<Result> DeleteTicketAsync(Guid ticketID);
    }
}
