using Domain.Base;
using Domain.Persons;
using System;

namespace Domain.Users
{
    public class User : BasicEntity
    {
        public Guid PersonId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Person Person { get; set; }
    }
}