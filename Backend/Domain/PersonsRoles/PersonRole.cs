using Domain.Base;
using Domain.Persons;
using Domain.Roles;
using System;

namespace Domain.PersonsRoles
{
    public class PersonRole : BasicEntity
    {
        public Guid RoleId { get; set; }
        public Guid PersonId { get; set; }

        public virtual Role Role { get; set; }
        public virtual Person Person { get; set; }
    }
}