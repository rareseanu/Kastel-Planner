using System;

namespace Application.BeneficiaryWeeklyLogs.Requests
{
    public class GetBeneficiaryWeeklyLogRequest
    { 
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
