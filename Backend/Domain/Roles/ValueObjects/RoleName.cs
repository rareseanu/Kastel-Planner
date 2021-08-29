using Domain.Base;
using System.Collections.Generic;

namespace Domain.Roles.ValueObjects
{
    public sealed class RoleName : ValueObject
    {
        private RoleName(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static Result<RoleName> Create(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return Result.Failure<RoleName>("Role name was not provided.");
            }

            return Result.Success(new RoleName(roleName));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
