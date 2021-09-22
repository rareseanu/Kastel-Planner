using Domain.Base;
using Domain.RefreshTokens.ValueObjects;
using Domain.Users;
using System;

namespace Domain.ResetPasswordTokens
{
    public class ResetPasswordToken : BasicEntity
    {
        public Token Token { get; set; }
        public DateTime ExpiresAt { get; set; }

        // Navigation properties
        public Guid UserId { get; set; }
        public User User { get; set; }

        private ResetPasswordToken()
        {
        }

        public ResetPasswordToken(Token token, DateTime expiresAt, Guid userId)
        {
            Id = Guid.NewGuid();
            Token = token;
            ExpiresAt = expiresAt;
            UserId = userId;
        }
    }
}
