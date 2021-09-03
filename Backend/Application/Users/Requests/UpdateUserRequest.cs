using System;

namespace Application.Users.Requests
{
    public class UpdateUserRequest
    {
        public Guid PersonId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
