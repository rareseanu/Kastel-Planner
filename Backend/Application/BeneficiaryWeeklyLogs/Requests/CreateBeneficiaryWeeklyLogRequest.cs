using System;

namespace Application.BeneficiaryWeeklyLogs.Requests
{
    public class CreateBeneficiaryWeeklyLogRequest
    {
        public TimeSpan StartTime { get; set; }
        public string DayOfWeek { get; set; }
        public Guid BeneficiaryId { get; set; }
    }
}
