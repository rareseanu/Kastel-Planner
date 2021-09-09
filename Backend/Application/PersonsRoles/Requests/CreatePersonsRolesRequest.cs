using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PersonsRoles.Requests
{
    public class CreatePersonsRolesRequest
    {
        public Guid RoleId { get; set; }
        public Guid PersonId { get; set; }
    }
}
