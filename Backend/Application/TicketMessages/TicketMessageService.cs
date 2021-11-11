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
        private readonly IUserRepository _userRepository;
        private readonly ITicketRepository _ticketRepository;

        public TicketMessageService(ITicketRepository ticketRepository, ITicketMessageRepository ticketMessageRepository,
                IUserRepository userRepository)
        {
            _ticketRepository = ticketRepository;
            _ticketMessageRepository = ticketMessageRepository;
            _userRepository = userRepository;
        }

        public async Task<Result<TicketMessageResponse>> CreateTicketMessageAsync(CreateTicketMessageRequest request)
        {
            var ticketMessage = new TicketMessage(request.Message, request.SentAt, request.UserId, request.TicketId);

            await _ticketMessageRepository.AddAsync(ticketMessage);

            var user = await _userRepository.GetFirstByPredicateAsync(u => u.Id.Equals(ticketMessage.UserId), u => u.Person);

            var ticketResponse = new TicketMessageResponse()
            {
                Id = ticketMessage.Id,
                Message = ticketMessage.Message,
                SentAt = ticketMessage.SentAt,
                UserId = ticketMessage.UserId,
                UserFirstName = user.Person.Name.FirstName,
                UserLastName = user.Person.Name.LastName,
                TicketId = ticketMessage.TicketId

            };

            return Result.Success(ticketResponse);
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
                var user = await _userRepository.GetFirstByPredicateAsync(u => u.Id.Equals(ticketMessage.UserId), u => u.Person);

                var ticketResponse = new TicketMessageResponse()
                {
                    Id = ticketMessage.Id,
                    Message = ticketMessage.Message,
                    SentAt = ticketMessage.SentAt,
                    UserId = ticketMessage.UserId,
                    UserFirstName = user.Person.Name.FirstName,
                    UserLastName = user.Person.Name.LastName,
                    TicketId = ticketMessage.TicketId

                };

                response.Add(ticketResponse);
            }
            return Result.Success<IList<TicketMessageResponse>>(response);
        }
    }
}
