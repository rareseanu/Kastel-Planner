using Domain.Base;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Domain.Users.ValueObjects
{
    public sealed class Email : ValueObject
    {
        private Email(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static Result<Email> Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return Result.Failure<Email>("Email was not provided.");
            }
            email = email.Trim();

            if (email.Length >= 256)
            {
                return Result.Failure<Email>("Email is too long");
            }
            if (!IsEmailValid(email))
            {
                return Result.Failure<Email>("Email is invalid.");
            }

            return Result.Success(new Email(email));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        private static bool IsEmailValid(string email)
        {
            const string pattern = @"^(.+)@(.+)$";
            const RegexOptions options = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;

            var matchTimeout = TimeSpan.FromSeconds(2);

            var regex = new Regex(pattern, options, matchTimeout);
            return regex.IsMatch(email);
        }
    }
}
