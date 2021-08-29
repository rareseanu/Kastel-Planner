using Domain.Base;
using Domain.PersonsRoles;
using Domain.Roles.ValueObjects;
using System;
using System.Collections.Generic;

namespace Domain.Roles
{
    public class Role : BasicEntity
    {
        public RoleName RoleName { get; set; }
        public ICollection<PersonRole> PersonRoles { get; set; }

        private Role()
        {
        }

        public Role(RoleName roleName)
        {
            Id = Guid.NewGuid();
            RoleName = roleName;
        }

        public void UpdateRole(RoleName roleName)
        {
            RoleName = roleName;
        }
    }
}