using Domain.Base;
using Domain.PersonsRoles;
using System.Collections.Generic;

namespace Domain.Roles
{
    public class Role : BasicEntity
    {
        public string RoleName { get; set; }

        public virtual ICollection<PersonRole> PersonRoles { get; set; }
    }
}