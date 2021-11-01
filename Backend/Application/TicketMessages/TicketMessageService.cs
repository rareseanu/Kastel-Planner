using Application.RepositoryInterfaces;
using Application.TicketMessages.Requests;
using Application.TicketMessages.Responses;
using Domain;
using Domain.TicketMessages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.TicketMessages
{
    public class TicketMessageService : ITicketMessageService
    {
        private readonly ITicketMessageRepository _ticketMessageRepository;
        private readonly ITicketRepository _ticketRepository;

        public TicketMessageService(ITicketRepository ticketRepository, ITicketMessageRepository ticketMessageRepository)
        {
            _ticketRepository = ticketRepository;
            _ticketMessageRepository = ticketMessageRepository;
        }

        public async Task<Result<TicketMessageResponse>> CreateTicketMessageAsync(CreateTicketMessageRequest request)
        {
            var ticketMessage = new TicketMessage(request.Message, request.SentAt, request.UserId, request.TicketId);

            await _ticketMessageRepository.AddAsync(ticketMessage);

            var response = new TicketMessageResponse()
            {
                Id = ticketMessage.Id,
                Message = ticketMessage.Message,
                SentAt = ticketMessage.SentAt,
                UserId = ticketMessage.UserId,
                TicketId = ticketMessage.TicketId
                
            };

            return Result.Success(response);
        }

        public async Task<Result<IList<TicketMessageResponse>>> GetTicketMessagesByTicketIdAsync(Guid ticketId)
        {
            var response = new List<TicketMessageResponse>();

            var ticket = await _ticketRepository.GetByIdAsync(ticketId);
            if (ticket == null)
            {
                return Result.Failure<IList<TicketMessageResponse>>($"Ticket with Id {ticketId} was not found");
            }

            var ticketMessages = await _ticketMessageRepository.GetAllByPredicateAsync(tm=> tm.TicketId.Equals(ticketId));
            foreach (var ticketMessage in ticketMessages)
            {
                var ticketResponse = new TicketMessageResponse()
                {
                    Id = ticketMessage.Id,
                    Message = ticketMessage.Message,
                    SentAt = ticketMessage.SentAt,
                    UserId = ticketMessage.UserId,
                    TicketId = ticketMessage.TicketId

                };

                response.Add(ticketResponse);
            }
            return Result.Success<IList<TicketMessageResponse>>(response);
        }
    }
}
