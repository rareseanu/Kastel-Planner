using Domain.Base;
using Domain.Persons;
using Domain.RefreshTokens;
using Domain.ResetPasswordTokens;
using Domain.TicketMessages;
using Domain.Tickets;
using Domain.Users.ValueObjects;
using System;
using System.Collections.Generic;

namespace Domain.Users
{
    public class User : BasicEntity
    {
        public Email Email { get; set; }
        public Password Password { get; set; }

        // Navigation properties
        public Person Person { get; set; }
        public Guid PersonId { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
        public ICollection<ResetPasswordToken> ResetPasswordTokens { get; set; }
        public ICollection<TicketMessage> TicketMessages { get; set; }
        public ICollection<Ticket> Tickets { get; set; }

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