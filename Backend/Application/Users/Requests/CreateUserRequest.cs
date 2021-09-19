using System;

namespace Application.Users.Requests
{
    public class CreateUserRequest
    {
        public Guid PersonId { get; set; }
        public string Email { get; set; }
    }
}
