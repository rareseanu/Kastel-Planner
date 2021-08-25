using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kastel_Planner_Backend.Models
{
    public class PersonRole
    {
        public int Id { get; set; }
        public Nullable<int> RoleId { get; set; }
        public Nullable<int> PersonId { get; set; }

        public virtual Role Role { get; set; }
        public virtual Person Person { get; set; }
    }
}