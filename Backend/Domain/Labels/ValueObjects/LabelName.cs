using Domain.Base;
using System.Collections.Generic;

namespace Domain.Labels.ValueObjects
{
    public sealed class LabelName : ValueObject
    {
        private LabelName(string value)
        {
            Value = value;
        }
        public string Value { get; }

        public static Result<LabelName> Create(string labelName)
        {
            if (string.IsNullOrWhiteSpace(labelName))
            {
                return Result.Failure<LabelName>("Label name was not provided.");
            }

            return Result.Success(new LabelName(labelName));
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
