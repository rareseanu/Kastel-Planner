using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;

namespace Kastel_Planner_Backend.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public Nullable<int> VolunteerId { get; set; }
        public Nullable<int> WeeklyLogId { get; set; }
        public DateTime Date;

        [RegexValidator(@"^[0-9]*$")]
        public int Duration;

        public virtual Person Volunteer { get; set; }
        public virtual BeneficiaryWeeklyLog BeneficiaryWeeklyLog { get; set; }

       
    }
}