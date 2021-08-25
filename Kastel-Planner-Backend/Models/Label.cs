using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kastel_Planner_Backend.Models
{
    public class Label
    {
        public int Id { get; set; }
        public string LabelName { get; set; }

        public virtual ICollection<PersonLabel> PersonLabels { get; set; }
    }
}