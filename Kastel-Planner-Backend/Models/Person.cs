using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Kastel_Planner_Backend.Models
{
    public class Person
    {
        public int Id { get; set; }

        [RegexValidator(@"^[a-zA-Z]*$")]
        public string FirstName { get; set; }

        [RegexValidator(@"^[a-zA-Z]*$")]
        public string LastName { get; set; }

        [RegexValidator(@"^[0-9]*$", MessageTemplate = "Phone Number must be numeric that starts with 0 digit")]
        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<PersonLabel> PersonLabels { get; set; }
        public virtual ICollection<PersonRole> PersonRoles { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<BeneficiaryWeeklyLog> BeneficiaryWeeklyLogs { get; set; }
    }
}