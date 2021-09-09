using Domain.Labels.ValueObjects;
using System;

namespace Application.Labels.Responses
{
    public class LabelResponse
    {
        public Guid Id { get; set; }
        public string LabelName { get; set; }
    }
}
