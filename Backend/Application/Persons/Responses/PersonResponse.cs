using Application.Labels.Responses;
using Application.Roles.Responses;
using System;

namespace Application.Persons.Responses
{
    public class PersonResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public RoleResponse[] Roles {get; set;}
        public LabelResponse[] Labels { get; set; }

    }
}
