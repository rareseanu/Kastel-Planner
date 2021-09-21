using System;

namespace Application.Schedules.Requests
{
    public class GetSchedulesRequest
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
