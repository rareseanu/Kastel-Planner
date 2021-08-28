using Domain.Persons;
using Domain.Schedules;
using System;
using System.Collections.Generic;

namespace Domain.BeneficiaryWeeklyLogs
{
    public class BeneficiaryWeeklyLog
    {
        public int Id { get; set; }

        public Nullable<int> BeneficiaryId { get; set; }

        public int StartTime { get; set; }

        public string DayOfWeek { get; set; }

        public virtual Person Person { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}