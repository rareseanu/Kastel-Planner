using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PersonsLabels.Requests
{
    public class UpdatePersonsLabelsRequest
    {
        public Guid LabelId { get; set; }
        public Guid PersonId { get; set; }
    }
}
