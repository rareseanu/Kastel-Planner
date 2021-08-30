using Domain.Base;
using Domain.BeneficiaryWeeklyLogs;
using Domain.Persons.ValueObjects;
using Domain.PersonsLabels;
using Domain.PersonsRoles;
using Domain.Schedules;
using Domain.Users;
using System;
using System.Collections.Generic;

namespace Domain.Persons
{
    public class Person : BasicEntity
    {
        public Name Name { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public bool IsActive { get; set; }

        // Navigation properties
        public User User { get; set; }
        public ICollection<PersonLabel> PersonLabels { get; set; }
        public ICollection<PersonRole> PersonRoles { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
        public ICollection<BeneficiaryWeeklyLog> BeneficiaryWeeklyLogs { get; set; }

        private Person()
        {
        }

        public Person(Name name, PhoneNumber phoneNumber, bool isActive)
        {
            Id = Guid.NewGuid();
            Name = name;
            PhoneNumber = phoneNumber;
            IsActive = isActive;
        }

        public void UpdatePerson(Name name, PhoneNumber phoneNumber, bool isActive)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            IsActive = isActive;
        }
    }
}
