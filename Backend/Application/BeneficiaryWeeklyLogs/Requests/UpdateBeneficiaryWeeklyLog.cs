using System;

namespace Application.BeneficiaryWeeklyLogs.Requests
{
    public class UpdateBeneficiaryWeeklyLog
    {
        public TimeSpan StartTime { get; set; }
        public string DayOfWeek { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public Guid BeneficiaryId { get; set; }
    }
}
