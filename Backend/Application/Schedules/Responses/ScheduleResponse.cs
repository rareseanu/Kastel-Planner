using Application.BeneficiaryWeeklyLogs.Responses;
using Domain.BeneficiaryWeeklyLogs;
using System;

namespace Application.Schedules.Responses
{
    public class ScheduleResponse
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid? VolunteerId { get; set; }
        public string VolunteerFirstName { get; set; }
        public string VolunteerLastName { get; set; }
        public Guid WeeklyLogId { get; set; }
        public BeneficiaryWeeklyLogResponse WeeklyLog { get; set; }
    }
}
