using Domain.Base;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Domain.Persons.ValueObjects
{
    public sealed class Name : ValueObject
    {
        private Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; }
        public string LastName { get; }

        public static Result<Name> Create(string firstName, string lastName)
        {
            const string nameRegex = @"^[a-zA-Z]+$";

            if (string.IsNullOrWhiteSpace(firstName))
            {
                return Result.Failure<Name>("First name was not provided.");
            }

            if (!Regex.IsMatch(firstName, nameRegex))
            {
                return Result.Failure<Name>("First name invalid.");
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                return Result.Failure<Name>("Last name was not provided.");
            }

            if (!Regex.IsMatch(lastName, nameRegex))
            {
                return Result.Failure<Name>("Last name invalid.");
            }

            return Result.Success(new Name(firstName, lastName));
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}
