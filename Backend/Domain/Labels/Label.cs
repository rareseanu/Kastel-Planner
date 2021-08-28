using Domain.PersonsLabels;
using System.Collections.Generic;

namespace Domain.Labels
{
    public class Label
    {
        public int Id { get; set; }
        public string LabelName { get; set; }

        public virtual ICollection<PersonLabel> PersonLabels { get; set; }
    }
}