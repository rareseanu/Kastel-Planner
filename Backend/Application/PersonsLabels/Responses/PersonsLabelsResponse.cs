using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PersonsLabels.Responses
{
    public class PersonsLabelsResponse
    {
        public Guid Id { get; set; }
        public Guid LabelId { get; set; }
        public Guid PersonId { get; set; }
    }
}
