using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BeneficiaryWeeklyLogs.Responses
{
    public class BeneficiaryWeeklyLogResponse
    {
        public Guid Id { get; set; }
        public TimeSpan StartTime { get; set; }
        public Domain.BeneficiaryWeeklyLogs.ValueObjects.DayOfWeek DayOfWeek { get; set; }
        public Guid BeneficiaryId { get; set; }
    }
}
