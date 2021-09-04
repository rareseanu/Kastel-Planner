using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.BeneficiaryWeeklyLogs.ValueObjects
{
    public sealed class DayOfWeek : ValueObject
    {
        public static DayOfWeek Monday { get; } = new DayOfWeek(0, "Monday");
        public static DayOfWeek Tuesday { get; } = new DayOfWeek(1, "Tuesday");
        public static DayOfWeek Wednesday { get; } = new DayOfWeek(2, "Wednesday");
        public static DayOfWeek Thursday { get; } = new DayOfWeek(3, "Thursday");
        public static DayOfWeek Friday { get; } = new DayOfWeek(4, "Friday");
        public static DayOfWeek Saturday { get; } = new DayOfWeek(5, "Saturday");
        public static DayOfWeek Sunday { get; } = new DayOfWeek(6, "Sunday");
        private DayOfWeek(int value, string name) 
        {
            Value = value;
            Name = name;
        }
        public int Value { get; }
        public string Name { get; }

        public static IEnumerable<DayOfWeek> List()
        {
            return new[] { Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday};
        }
        public static DayOfWeek FromString(string dayOfWeek)
        {
            return List().SingleOrDefault(r => String.Equals(r.Name, dayOfWeek, StringComparison.OrdinalIgnoreCase));
        }

        public static DayOfWeek FromValue(int value)
        {
            return List().SingleOrDefault(r => r.Value == value);
        }

        public static Result<DayOfWeek> Create(string dayOfWeek)
        {
            DayOfWeek createdDayOfWeek = FromString(dayOfWeek);
            if (createdDayOfWeek == null)
            {
                return Result.Failure<DayOfWeek>("Invalid day of the week.");
            }

            return Result.Success(createdDayOfWeek);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}
