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
        public Label Label { get; set; }
        public Person Person { get; set; }

        private PersonLabel()
        {
        }

        public PersonLabel(Guid labelId, Guid personId)
        {
            Id = Guid.NewGuid();
            LabelId = labelId;
            PersonId = personId;
        }

        public void UpdatePersonLabel(Guid labelId, Guid personId)
        {
            LabelId = labelId;
            PersonId = personId;
        }
    }
}