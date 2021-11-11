using System;

namespace Application.Tickets.Responses
{
    public class TicketResponse
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime OpenedDate { get; set; }
        public string Type { get; set; }
        public Guid UserID { get; set; }
    }
}
