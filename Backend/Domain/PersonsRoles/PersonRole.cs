using Domain.Persons;
using Domain.Roles;
using System;

namespace Domain.PersonsRoles
{
    public class PersonRole
    {
        public int Id { get; set; }
        public Nullable<int> RoleId { get; set; }
        public Nullable<int> PersonId { get; set; }

        public virtual Role Role { get; set; }
        public virtual Person Person { get; set; }
    }
}