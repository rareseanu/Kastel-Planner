using Domain.Base;
using Domain.Labels.ValueObjects;
using Domain.PersonsLabels;
using System;
using System.Collections.Generic;

namespace Domain.Labels
{
    public class Label : BasicEntity
    {
        public LabelName LabelName { get; set; }
        public ICollection<PersonLabel> PersonLabels { get; set; }

        private Label()
        {
        }

        public Label(LabelName labelName)
        {
            Id = Guid.NewGuid();
            LabelName = labelName;
        }

        public void UpdateLabel(LabelName labelName)
        {
            LabelName = labelName;
        }
    }
}