using Domain.Base;
using Domain.BeneficiaryWeeklyLogs;
using Domain.Persons;
using System;

namespace Domain.Schedules
{
    public class Schedule : BasicEntity
    {
        public Guid VolunteerId { get; set; }
        public Guid WeeklyLogId { get; set; }

        public DateTime Date;

        [RegexValidator(@"^[0-9]*$")]
        public int Duration;

        public virtual Person Volunteer { get; set; }
        public virtual BeneficiaryWeeklyLog BeneficiaryWeeklyLog { get; set; }
    }
}