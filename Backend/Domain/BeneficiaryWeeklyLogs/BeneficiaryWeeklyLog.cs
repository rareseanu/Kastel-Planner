using Domain.Base;
using Domain.Persons;
using Domain.Schedules;
using System;
using System.Collections.Generic;

namespace Domain.BeneficiaryWeeklyLogs
{
    public class BeneficiaryWeeklyLog : BasicEntity
    {
        public Guid BeneficiaryId { get; set; }

        public DateTime StartTime { get; set; }

        public string DayOfWeek { get; set; }

        public virtual Person Person { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}