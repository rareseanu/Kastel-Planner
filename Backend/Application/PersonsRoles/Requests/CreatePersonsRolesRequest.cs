using System;

namespace Application.PersonsRoles.Requests
{
    public class CreatePersonsRolesRequest
    {
        public Guid RoleId { get; set; }
        public Guid PersonId { get; set; }
    }
}
