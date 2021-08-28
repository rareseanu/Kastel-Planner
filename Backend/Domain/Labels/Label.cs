using Domain.Base;
using Domain.PersonsLabels;
using System.Collections.Generic;

namespace Domain.Labels
{
    public class Label : BasicEntity
    {
        public string LabelName { get; set; }

        public virtual ICollection<PersonLabel> PersonLabels { get; set; }
    }
}