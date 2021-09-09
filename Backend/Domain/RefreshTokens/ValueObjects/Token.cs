using Domain.Base;
using System.Collections.Generic;

namespace Domain.RefreshTokens.ValueObjects
{
    public sealed class Token : ValueObject
    {
        private Token(string value)
        {
            Value = value;
        }

        public static Result<Token> Create(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return Result.Failure<Token>("Refresh token was not provided.");
            }
            return Result.Success(new Token(token));
        }

        public string Value { get; set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
