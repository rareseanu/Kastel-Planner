using Domain.Base;
using System;

namespace Domain.TicketMessages
{
    public class TicketMessage : BasicEntity
    {
        public string Message { get; set; }
        public DateTime SentAt { get; set; }

        // Navigation properties
        public Guid UserId { get; set; }
        public Guid TicketId { get; set; }

        private TicketMessage() { }

        public TicketMessage(string message, DateTime sentAt, Guid userID, Guid ticketID) 
        {
            Id = Guid.NewGuid();
            Message = message;
            SentAt = sentAt;
            UserId = userID;
            TicketId = ticketID;
        }

        public void UpdateTicketMessage(string message, DateTime sentAt, Guid userID, Guid ticketID)
        {
            Message = message;
            SentAt = sentAt;
            UserId = userID;
            TicketId = ticketID;
        }
    }
}
