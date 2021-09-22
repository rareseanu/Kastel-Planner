using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Schedules.Requests
{
    public class UpdateScheduleRequest
    {
        public DateTime Date { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public Guid? VolunteerId { get; set; }
        public Guid WeeklyLogId { get; set; }
    }
}
