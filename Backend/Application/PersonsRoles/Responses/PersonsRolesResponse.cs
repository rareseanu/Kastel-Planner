using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PersonsRoles.Responses
{
    public class PersonsRolesResponse
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid PersonId { get; set; }
    }
}
