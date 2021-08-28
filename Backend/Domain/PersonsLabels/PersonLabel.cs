using Domain.Base;
using Domain.Labels;
using Domain.Persons;
using System;

namespace Domain.PersonsLabels
{
    public class PersonLabel : BasicEntity
    {
        public Guid LabelId { get; set; }
        public Guid PersonId { get; set; }

        public virtual Label Label { get; set; }
        public virtual Person Person { get; set; }
    }
}