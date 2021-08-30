using Domain.Base;
using System.Collections.Generic;

namespace Domain.Schedules.ValueObjects
{
    public sealed class Duration : ValueObject
    {
        private Duration(int minutes)
        {
            Minutes = minutes;
        }
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

            minutes += hours * 60;

            return Result.Success(new Duration(minutes));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Minutes;
        }
    }
}
