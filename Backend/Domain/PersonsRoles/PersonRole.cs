using Domain.Base;
using Domain.Persons;
using Domain.Roles;
using System;

namespace Domain.PersonsRoles
{
    public class PersonRole : BasicEntity
    {
        // Navigation properties
        public Guid RoleId { get; set; }
        public Guid PersonId { get; set; }
        public Role Role { get; set; }
        public Person Person { get; set; }

        private PersonRole()
        {
        }

        public PersonRole(Guid roleId, Guid personId)
        {
            Id = Guid.NewGuid();
            RoleId = roleId;
            PersonId = personId;
        }

        public void UpdatePersonRole(Guid roleId, Guid personId)
        {
            RoleId = roleId;
            PersonId = personId;
        }
    }
}