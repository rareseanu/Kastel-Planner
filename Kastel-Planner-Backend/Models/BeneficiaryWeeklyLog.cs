using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kastel_Planner_Backend.Models
{
    public class BeneficiaryWeeklyLog
    {
        public int Id { get; set; }

        public Nullable<int> BeneficiaryId { get; set; }

        public int StartTime { get; set; }

        public string DayOfWeek { get; set; }

        public virtual Person Person { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}