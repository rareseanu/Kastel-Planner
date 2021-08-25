using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kastel_Planner_Backend.Models
{
    public class PersonLabel
    {
        public int Id { get; set; }

        public Nullable<int> LabelId { get; set; }
        public Nullable<int> PersonId { get; set; }

        public virtual Label Label { get; set; }
        public virtual Person Person { get; set; }
    }
}