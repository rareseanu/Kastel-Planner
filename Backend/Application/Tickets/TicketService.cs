using Application.RepositoryInterfaces;
using Application.Tickets.Requests;
using Application.Tickets.Responses;
using Domain;
using Domain.Tickets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Tickets
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;

        public TicketService(ITicketRepository ticketRepository, IUserRepository userRepository)
        {
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
        }

        public async Task<Result<TicketResponse>> CreateTicketAsync(CreateTicketRequest request)
        {
            var ticket = new Ticket(request.Subject, request.Status, request.OpenedDate, request.Type, request.UserID);
            
            await _ticketRepository.AddAsync(ticket);

            var response = new TicketResponse()
            {
                Id = ticket.Id,
                Subject = ticket.Subject,
                Status = ticket.Status,
                OpenedDate = ticket.OpenedDate,
                Type = ticket.Type,
                UserID = ticket.UserId
            };

            return Result.Success(response);
        }

        public Task<Result> DeleteTicketAsync(Guid ticketID)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<TicketResponse>> GetAllTicketsAsync()
        {
            var response = new List<TicketResponse>();

            var tickets = await _ticketRepository.GetAllAsync();
            foreach (var ticket in tickets)
            {
                var ticketResponse = new TicketResponse()
                {
                    Id = ticket.Id,
                    Subject = ticket.Subject,
                    Status = ticket.Status,
                    OpenedDate = ticket.OpenedDate,
                    Type = ticket.Type,
                    UserID = ticket.UserId
                };

                response.Add(ticketResponse);
            }
            return response;
        }

        public Task<Result<TicketResponse>> GetTicketByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<IList<TicketResponse>>> GetTicketsByUserIdAsync(Guid userId)
        {
            var response = new List<TicketResponse>();

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return Result.Failure<IList<TicketResponse>>($"User with Id {userId} was not found");
            }

            var tickets = await _ticketRepository.GetAllByPredicateAsync(t => t.UserId.Equals(userId));
            foreach (var ticket in tickets)
            {
                var ticketResponse = new TicketResponse()
                {
                    Id = ticket.Id,
                    Subject = ticket.Subject,
                    Status = ticket.Status,
                    OpenedDate = ticket.OpenedDate,
                    Type = ticket.Type,
                    UserID = ticket.UserId
                };

                response.Add(ticketResponse);
            }
            return Result.Success<IList<TicketResponse>>(response);
        }

        public Task<Result<TicketResponse>> UpdateTicketAsync(Guid ticketID, UpdateTicketRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
