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

            return Result.Success(new PhoneNumber(number));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
    }
}
