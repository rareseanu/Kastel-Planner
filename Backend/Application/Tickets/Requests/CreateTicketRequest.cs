using System;

namespace Application.Tickets.Requests
{
    public class CreateTicketRequest
    {
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime OpenedDate { get; set; }
        public string Type { get; set; }
        public Guid UserID { get; set; }
    }
}
