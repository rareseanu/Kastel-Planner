using System;

namespace Application.Schedules.Requests
{
    public class CreateScheduleRequest
    {
        public DateTime Date { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public Guid VolunteerId { get; set; }
        public Guid WeeklyLogId { get; set; }
    }
}
