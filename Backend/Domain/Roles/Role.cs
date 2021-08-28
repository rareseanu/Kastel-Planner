using Domain.PersonsRoles;
using System.Collections.Generic;

namespace Domain.Roles
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<PersonRole> PersonRoles { get; set; }
    }
}