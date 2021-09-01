using Domain.Base;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Domain.Persons.ValueObjects
{
    public sealed class PhoneNumber : ValueObject
    {
        private PhoneNumber(string number)
        {
            Number = number;
        }
        public string Number { get; }

        public static Result<PhoneNumber> Create(string number)
        {
            const string phoneNumberRegex = @"^\d{10}$"; // Ex: 0761234567

            if (string.IsNullOrWhiteSpace(number))
            {
                return Result.Failure<PhoneNumber>("Phone number was not provided.");
            }

            if(!Regex.IsMatch(number, phoneNumberRegex))
            {
                return Result.Failure<PhoneNumber>("Invalid phone number format.");
            }

            return Result.Success(new PhoneNumber(number));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
    }
}
