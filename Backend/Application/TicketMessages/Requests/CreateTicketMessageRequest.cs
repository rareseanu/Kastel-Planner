using System;

namespace Application.TicketMessages.Requests
{
    public class CreateTicketMessageRequest
    {
        public string Message { get; set; }
        public DateTime SentAt { get; set; }
        public Guid UserId { get; set; }
        public Guid TicketId { get; set; }
    }
}
