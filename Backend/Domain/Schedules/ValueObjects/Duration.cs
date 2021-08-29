using Domain.Base;
using System.Collections.Generic;

namespace Domain.Schedules.ValueObjects
{
    public sealed class Duration : ValueObject
    {
        private Duration(int hours, int minutes)
        {
            Hours = hours;
            Minutes = minutes;
        }
        public int Hours { get; }
        public int Minutes { get; }

        public static Result<Duration> Create(int hours, int minutes)
        {
            if(hours < 0)
            {
                return Result.Failure<Duration>("Invalid number of hours.");
            }
            if(minutes < 0)
            {
                return Result.Failure<Duration>("Invalid number of minutes.");
            }
            if(hours == 0 && minutes == 0)
            {
                return Result.Failure<Duration>("Invalid duration.");
            }

            if(minutes > 0)
            {
                int convertedHours = minutes / 60;
                hours += convertedHours;
                minutes -= convertedHours * 60;
            }

            return Result.Success(new Duration(hours, minutes));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Hours;
            yield return Minutes;
        }
    }
}
