using System;

namespace Application.Users.Responses
{
    public class AuthenticateResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expires { get; set; }
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public string Email { get; set; }
        public string[] Roles { get; set; }
    }
}
