using System;

namespace Application.BeneficiaryWeeklyLogs.Requests
{
    public class CreateBeneficiaryWeeklyLogRequest
    {
        public TimeSpan StartTime { get; set; }
        public String DayOfWeek { get; set; }
        public Guid BeneficiaryId { get; set; }
    }
}
