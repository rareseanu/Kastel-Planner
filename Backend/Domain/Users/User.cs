using Domain.Base;
using Domain.Persons;
using Domain.Users.ValueObjects;
using System;

namespace Domain.Users
{
    public class User : BasicEntity
    {
        public Guid PersonId { get; set; }
        public Email Email { get; set; }

        // Navigation properties
        public Person Person { get; set; }

        private User()
        {
        }

        public User(Guid personId, Email email)
        {
            Id = Guid.NewGuid();
            PersonId = personId;
            Email = email;
        }

        public void UpdateUser(Guid personId, Email email)
        {
            PersonId = personId;
            Email = email;
        }
    }
}