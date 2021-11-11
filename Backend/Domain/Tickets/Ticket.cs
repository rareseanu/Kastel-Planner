using Domain.Base;
using Domain.TicketMessages;
using System;
using System.Collections.Generic;

namespace Domain.Tickets
{
    public class Ticket : BasicEntity
    {
        public string Subject { get; set; }

        // Could be enum like weekdays
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime OpenedDate { get; set; }
        // Feature request / functionality bug (could be enum)
        public string Type { get; set; }

        // Navigation properties
        public ICollection<TicketMessage> TicketMessages { get; set; }
        public Guid UserId { get; set; }

        private Ticket() { }

        public Ticket(string subject, string status, DateTime openedDate, string type, Guid userID)
        {
            Id = Guid.NewGuid();
            Subject = subject;
            Status = status;
            OpenedDate = openedDate;
            Type = type;
            UserId = userID;
        }

        public void UpdateTicket(string subject, string status, DateTime openedDate, string type, Guid userID)
        {
            Subject = subject;
            Status = status;
            OpenedDate = openedDate;
            Type = type;
            UserId = userID;
        }

    }
}
