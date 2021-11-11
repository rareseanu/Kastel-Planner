using System;

namespace Application.TicketMessages.Responses
{
    public class TicketMessageResponse
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTime SentAt { get; set; }
        public Guid UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public Guid TicketId { get; set; }
    }
}
