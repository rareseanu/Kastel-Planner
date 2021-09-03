using System;

namespace Application.Users.Responses
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public string Email { get; set; }
    }
}
