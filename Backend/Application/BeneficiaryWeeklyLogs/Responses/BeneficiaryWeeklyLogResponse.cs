using Application.Schedules.Responses;
using Domain.Schedules;
using System;

namespace Application.BeneficiaryWeeklyLogs.Responses
{
    public class BeneficiaryWeeklyLogResponse
    {
        public Guid Id { get; set; }
        public TimeSpan StartTime { get; set; }
        public Domain.BeneficiaryWeeklyLogs.ValueObjects.DayOfWeek DayOfWeek { get; set; }
        public Guid BeneficiaryId { get; set; }
        public string BeneficiaryFirstName { get; set; }
        public string BeneficiaryLastName { get; set; }
        public int Duration { get; set; }
        public ScheduleResponse Schedule { get; set; }
    }
}
