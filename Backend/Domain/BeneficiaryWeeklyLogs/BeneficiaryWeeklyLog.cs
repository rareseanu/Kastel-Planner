using Domain.Base;
using Domain.BeneficiaryWeeklyLogs.ValueObjects;
using Domain.Persons;
using Domain.Schedules;
using System;
using System.Collections.Generic;

namespace Domain.BeneficiaryWeeklyLogs
{
    public class BeneficiaryWeeklyLog : BasicEntity
    {

        public TimeSpan StartTime { get; set; }
        public ValueObjects.DayOfWeek DayOfWeek { get; set; }
        public Duration Duration { get; set; }

        // Navigation properties
        public Guid BeneficiaryId { get; set; }
        public Person Person { get; set; }
        public ICollection<Schedule> Schedules { get; set; }

        private BeneficiaryWeeklyLog()
        {
        }

        public BeneficiaryWeeklyLog(Guid beneficiaryId, TimeSpan startTime,
                ValueObjects.DayOfWeek dayOfWeek, Duration duration)
        {
            Id = Guid.NewGuid();
            BeneficiaryId = beneficiaryId;
            StartTime = startTime;
            DayOfWeek = dayOfWeek;
            Duration = duration;
        }

        public void UpdateBeneficiaryWeeklyLog(Guid beneficiaryId, TimeSpan startTime,
                ValueObjects.DayOfWeek dayOfWeek, Duration duration)
        {
            BeneficiaryId = beneficiaryId;
            StartTime = startTime;
            DayOfWeek = dayOfWeek;
            Duration = duration;
        }
    }
}