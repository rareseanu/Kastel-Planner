using Domain.BeneficiaryWeeklyLogs;
using Domain.Labels;
using Domain.Labels.ValueObjects;
using Domain.Persons;
using Domain.Persons.ValueObjects;
using Domain.PersonsLabels;
using Domain.PersonsRoles;
using Domain.Roles;
using Domain.Roles.ValueObjects;
using Domain.Schedules;
using Domain.Schedules.ValueObjects;
using Domain.Users;
using Domain.Users.ValueObjects;
using Infrastructure.Context;
using System;
using System.Linq;
using System.Threading.Tasks;
using DayOfWeek = Domain.BeneficiaryWeeklyLogs.ValueObjects.DayOfWeek;

namespace Infrastructure.Migrations
{
    public static class DbInitializer
    {
        public static async Task Initialize(KastelPlannerDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            var personDbSet = context.Set<Person>();
            var usersDbSet = context.Set<User>();
            var roleDbSet = context.Set<Role>();
            var labelDbSet = context.Set<Label>();
            var scheduleDbSet = context.Set<Schedule>();
            var weeklyLogDbSet = context.Set<BeneficiaryWeeklyLog>();
            var personRoleDbSet = context.Set<PersonRole>();
            var personLabelDbSet = context.Set<PersonLabel>();

            if (personDbSet.Any())
            {
                return;
            }

            var persons = new Person[]
            {
                CreatePerson("Bob", "Bob", "0123456789"),
                CreatePerson("John", "John", "9876543210")
            };
            await personDbSet.AddRangeAsync(persons);
            await context.SaveChangesAsync();

            var users = new User[]
            {
                CreateUser(personDbSet.Single(i => i.Name.FirstName.Equals("Bob")).Id, "bob@gmail.com", "bob123456")
            };
            await usersDbSet.AddRangeAsync(users);
            await context.SaveChangesAsync();

            var roles = new Role[] 
            {
                CreateRole("Admin"),
                CreateRole("Volunteer"),
                CreateRole("Beneficiary")
            };
            await roleDbSet.AddRangeAsync(roles);
            await context.SaveChangesAsync();

            var personRoles = new PersonRole[]
            {
                new PersonRole
                (
                    roleDbSet.Single(i => i.RoleName.Value.Equals("Volunteer")).Id,
                    personDbSet.Single(i => i.Name.FirstName.Equals("Bob")).Id
                ),
                new PersonRole
                (
                    roleDbSet.Single(i => i.RoleName.Value.Equals("Beneficiary")).Id,
                    personDbSet.Single(i => i.Name.FirstName.Equals("John")).Id
                )
            };
            await personRoleDbSet.AddRangeAsync(personRoles);
            await context.SaveChangesAsync();

            var labels = new Label[]
            {
                CreateLabel("Physical disabilities"),
                CreateLabel("Mental health disorders")
            };
            await labelDbSet.AddRangeAsync(labels);
            await context.SaveChangesAsync();

            var personLabels = new PersonLabel[]
            {
                new PersonLabel
                (
                    personDbSet.Single(i => i.Name.FirstName.Equals("Bob")).Id,
                    labelDbSet.Single(i => i.LabelName.Value.Equals("Physical disabilities")).Id
                    
                ),
                new PersonLabel
                (
                    personDbSet.Single(i => i.Name.FirstName.Equals("John")).Id,
                    labelDbSet.Single(i => i.LabelName.Value.Equals("Mental health disorders")).Id
                )
            };
            await personLabelDbSet.AddRangeAsync(personLabels);
            await context.SaveChangesAsync();

            var weeklyLogs = new BeneficiaryWeeklyLog[]
            {
                CreateWeeklyLogForBeneficiary(personDbSet.Single(i => i.Name.FirstName.Equals("John")).Id,
                    "Monday", new TimeSpan(1, 30, 0))

            };
            await weeklyLogDbSet.AddRangeAsync(weeklyLogs);
            await context.SaveChangesAsync();

            var schedules = new Schedule[]
            {
                new Schedule(
                    null,
                    weeklyLogDbSet.FirstOrDefault(i => i.Person.Name.FirstName.Equals("John")).Id,
                    DateTime.Now,
                    Duration.Create(2, 0).Value
                )
            };
            await scheduleDbSet.AddRangeAsync(schedules);
            await context.SaveChangesAsync();
        }

        private static Person CreatePerson(string firstName, string lastName, string phoneNumber)
        {
            Name namePerson = Name.Create(firstName, lastName).Value;
            PhoneNumber numberPerson = PhoneNumber.Create(phoneNumber).Value;

            Person person = new Person(namePerson, numberPerson, true);
            return person;
        }

        private static User CreateUser(Guid personId, string emailAddress, string password)
        {
            Email email = Email.Create(emailAddress).Value;
            Password passwordValueObj = Password.Create(password).Value;
            User user = new User(personId, email, passwordValueObj);

            return user;
        }

        private static Role CreateRole(string roleName)
        {
            RoleName r = RoleName.Create(roleName).Value;
            Role role = new Role(r);

            return role;
        }

        private static Label CreateLabel(string labelName)
        {
            LabelName labelName1 = LabelName.Create(labelName).Value;
            Label label = new Label(labelName1);
            return label;
        }

        private static BeneficiaryWeeklyLog CreateWeeklyLogForBeneficiary(Guid beneficiaryId, string dayOfWeek, TimeSpan startTime)
        {
            DayOfWeek day = DayOfWeek.Create(dayOfWeek).Value;
            BeneficiaryWeeklyLog beneficiaryWeeklyLog = new BeneficiaryWeeklyLog(beneficiaryId, startTime, day);

            return beneficiaryWeeklyLog;
        }
    }
}
