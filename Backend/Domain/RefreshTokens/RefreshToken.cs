using Domain.Base;
using Domain.RefreshTokens.ValueObjects;
using Domain.Users;
using System;

namespace Domain.RefreshTokens
{
    public class RefreshToken : BasicEntity
    {
        public Token Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? RevokedAt { get; set; }

        // Navigation properties
        public Guid UserId { get; set; }
        public User User { get; set; }

        private RefreshToken()
        {
        }

        public RefreshToken(Token token, DateTime expiresAt, DateTime createdAt, Guid userId)
        {
            Id = Guid.NewGuid();
            Token = token;
            ExpiresAt = expiresAt;
            CreatedAt = createdAt;
            UserId = userId;
        }
    }
}
