using Domain.Labels;
using Domain.Persons;
using System;

namespace Domain.PersonsLabels
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