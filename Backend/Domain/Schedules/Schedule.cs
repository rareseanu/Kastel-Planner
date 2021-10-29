using Domain.Base;
using Domain.BeneficiaryWeeklyLogs;
using Domain.Persons;
using System;

namespace Domain.Schedules
{
    public class Schedule : BasicEntity
    {
        public DateTime Date { get; set; }

        // Navigation properties
        public Guid? VolunteerId { get; set; }
        public Guid WeeklyLogId { get; set; }
        public Person Volunteer { get; set; }
        public BeneficiaryWeeklyLog BeneficiaryWeeklyLog { get; set; }

        private Schedule()
        {
        }

        public Schedule(Guid? volunteerId, Guid weeklyLogId, DateTime date)
        {
            Id = Guid.NewGuid();
            VolunteerId = volunteerId;
            WeeklyLogId = weeklyLogId;
            Date = date;
        }

        public void UpdateSchedule(Guid? volunteerId, Guid weeklyLogId, DateTime date)
        {
            VolunteerId = volunteerId;
            WeeklyLogId = weeklyLogId;
            Date = date;
        }
    }
}