using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BeneficiaryWeeklyLogs.Requests
{
    public class UpdateBeneficiaryWeeklyLog
    {
        public TimeSpan StartTime { get; set; }
        public String DayOfWeek { get; set; }
        public Guid BeneficiaryId { get; set; }
    }
}
