using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Schedules.Responses
{
    public class ScheduleResponse
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public Guid? VolunteerId { get; set; }
        public string VolunteerFirstName { get; set; }
        public string VolunteerLastName { get; set; }
        public Guid WeeklyLogId { get; set; }
        public string BeneficiaryFirstName { get; set; }
        public string BeneficiaryLastName { get; set; }
        public Guid BeneficiaryId { get; set; }
        public TimeSpan StartTime { get; set; }
        public string DayOfWeek { get; set; }
    }
}
