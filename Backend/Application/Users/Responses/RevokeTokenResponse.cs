using System;

namespace Application.Users.Responses
{
    public class RevokeTokenResponse
    {
        public string RefreshToken { get; set; }
        public DateTime RevokedAt { get; set; }
    }
}
