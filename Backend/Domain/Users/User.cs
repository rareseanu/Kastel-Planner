using Domain.Base;
using Domain.Persons;
using Domain.Users.ValueObjects;
using System;

namespace Domain.Users
{
    public class User : BasicEntity
    {
        public Email Email { get; set; }

        // Navigation properties
        public Person Person { get; set; }
        public Guid PersonId { get; set; }
        public Password Password { get; set; }

        private User()
        {
        }

        public User(Guid personId, Email email , Password password)
        {
            Id = Guid.NewGuid();
            PersonId = personId;
            Email = email;
            Password = password;
        }

        public void UpdateUser(Guid personId, Email email, Password password)
        {
            PersonId = personId;
            Email = email;
            Password = password;
        }
    }
}