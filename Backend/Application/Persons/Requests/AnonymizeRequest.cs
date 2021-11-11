using System;

namespace Application.Persons.Requests
{
    public class AnonymizeRequest
    {
        public Guid PersonId { get; set;}
        public string[] Fields { get; set; }
    }
}
