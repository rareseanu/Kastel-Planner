using Domain.Base;
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

        // Navigation properties
        public Guid BeneficiaryId { get; set; }
        public Person Person { get; set; }
        public ICollection<Schedule> Schedules { get; set; }

        private BeneficiaryWeeklyLog()
        {
        }

        public BeneficiaryWeeklyLog(Guid beneficiaryId, TimeSpan startTime,
                ValueObjects.DayOfWeek dayOfWeek)
        {
            Id = Guid.NewGuid();
            BeneficiaryId = beneficiaryId;
            StartTime = startTime;
            DayOfWeek = dayOfWeek;
        }

        public void UpdateBeneficiaryWeeklyLog(Guid beneficiaryId, TimeSpan startTime,
                ValueObjects.DayOfWeek dayOfWeek)
        {
            BeneficiaryId = beneficiaryId;
            StartTime = startTime;
            DayOfWeek = dayOfWeek;
        }
    }
}