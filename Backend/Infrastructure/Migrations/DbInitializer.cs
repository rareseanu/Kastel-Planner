using Domain.BeneficiaryWeeklyLogs;
using Domain.BeneficiaryWeeklyLogs.ValueObjects;
using Domain.Labels;
using Domain.Labels.ValueObjects;
using Domain.Persons;
using Domain.Persons.ValueObjects;
using Domain.PersonsLabels;
using Domain.PersonsRoles;
using Domain.Roles;
using Domain.Roles.ValueObjects;
using Domain.Schedules;
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
                CreatePerson("Bob", "Admin", "0123456789"),
                CreatePerson("Andrei", "Voluntar", "0761234586"),
                CreatePerson("Cristina", "Voluntar", "0754182715"),
                CreatePerson("Matei", "Voluntar", "0724501825"),
                CreatePerson("Alexandru", "Beneficiar", ""),
                CreatePerson("Elena", "Beneficiar", "0761281745"),
                CreatePerson("Dragos", "Beneficiar", "0776401925"),
                CreatePerson("Roxana", "Beneficiar", ""),

            };
            await personDbSet.AddRangeAsync(persons);
            await context.SaveChangesAsync();

            var users = new User[]
            {
                CreateUser(personDbSet.Single(i => i.Name.FirstName.Equals("Bob")).Id, "bob@gmail.com", "bob123456"),
                CreateUser(personDbSet.Single(i => i.Name.FirstName.Equals("Andrei")).Id, "voluntar1@gmail.com", "voluntar123"),
                CreateUser(personDbSet.Single(i => i.Name.FirstName.Equals("Cristina")).Id, "voluntar2@gmail.com", "voluntar123"),
                CreateUser(personDbSet.Single(i => i.Name.FirstName.Equals("Matei")).Id, "voluntar3@gmail.com", "voluntar123")
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
                    roleDbSet.Single(i => i.RoleName.Value.Equals("Admin")).Id,
                    personDbSet.Single(i => i.Name.FirstName.Equals("Bob")).Id
                ),
                new PersonRole
                (
                    roleDbSet.Single(i => i.RoleName.Value.Equals("Volunteer")).Id,
                    personDbSet.Single(i => i.Name.FirstName.Equals("Cristina")).Id
                ),
                new PersonRole
                (
                    roleDbSet.Single(i => i.RoleName.Value.Equals("Volunteer")).Id,
                    personDbSet.Single(i => i.Name.FirstName.Equals("Andrei")).Id
                ),
                new PersonRole
                (
                    roleDbSet.Single(i => i.RoleName.Value.Equals("Volunteer")).Id,
                    personDbSet.Single(i => i.Name.FirstName.Equals("Matei")).Id
                ),
                new PersonRole
                (
                    roleDbSet.Single(i => i.RoleName.Value.Equals("Beneficiary")).Id,
                    personDbSet.Single(i => i.Name.FirstName.Equals("Alexandru")).Id
                ),
                new PersonRole
                (
                    roleDbSet.Single(i => i.RoleName.Value.Equals("Beneficiary")).Id,
                    personDbSet.Single(i => i.Name.FirstName.Equals("Elena")).Id
                ),
                new PersonRole
                (
                    roleDbSet.Single(i => i.RoleName.Value.Equals("Beneficiary")).Id,
                    personDbSet.Single(i => i.Name.FirstName.Equals("Dragos")).Id
                ),
                new PersonRole
                (
                    roleDbSet.Single(i => i.RoleName.Value.Equals("Beneficiary")).Id,
                    personDbSet.Single(i => i.Name.FirstName.Equals("Roxana")).Id
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
                    personDbSet.Single(i => i.Name.FirstName.Equals("Andrei")).Id,
                    labelDbSet.Single(i => i.LabelName.Value.Equals("Physical disabilities")).Id
                    
                ),
                new PersonLabel
                (
                    personDbSet.Single(i => i.Name.FirstName.Equals("Andrei")).Id,
                    labelDbSet.Single(i => i.LabelName.Value.Equals("Mental health disorders")).Id
                ),
                new PersonLabel
                (
                    personDbSet.Single(i => i.Name.FirstName.Equals("Dragos")).Id,
                    labelDbSet.Single(i => i.LabelName.Value.Equals("Physical disabilities")).Id
                )
            };
            await personLabelDbSet.AddRangeAsync(personLabels);
            await context.SaveChangesAsync();

            var weeklyLogs = new BeneficiaryWeeklyLog[]
            {
                CreateWeeklyLogForBeneficiary(personDbSet.Single(i => i.Name.FirstName.Equals("Alexandru")).Id,
                    "Friday", new TimeSpan(13, 30, 0), Duration.Create(2, 0).Value),
                CreateWeeklyLogForBeneficiary(personDbSet.Single(i => i.Name.FirstName.Equals("Elena")).Id,
                    "Friday", new TimeSpan(15, 0, 0), Duration.Create(2, 0).Value),
                CreateWeeklyLogForBeneficiary(personDbSet.Single(i => i.Name.FirstName.Equals("Dragos")).Id,
                    "Friday", new TimeSpan(11, 20, 0), Duration.Create(2, 0).Value),
                CreateWeeklyLogForBeneficiary(personDbSet.Single(i => i.Name.FirstName.Equals("Roxana")).Id,
                    "Wednesday", new TimeSpan(15, 30, 0), Duration.Create(2, 0).Value),
                CreateWeeklyLogForBeneficiary(personDbSet.Single(i => i.Name.FirstName.Equals("Roxana")).Id,
                    "Thursday", new TimeSpan(12, 0, 0), Duration.Create(2, 0).Value),
                CreateWeeklyLogForBeneficiary(personDbSet.Single(i => i.Name.FirstName.Equals("Dragos")).Id,
                    "Monday", new TimeSpan(13, 30, 0), Duration.Create(2, 0).Value),
                CreateWeeklyLogForBeneficiary(personDbSet.Single(i => i.Name.FirstName.Equals("Alexandru")).Id,
                    "Monday", new TimeSpan(15, 30, 0), Duration.Create(2, 0).Value),
                CreateWeeklyLogForBeneficiary(personDbSet.Single(i => i.Name.FirstName.Equals("Elena")).Id,
                    "Tuesday", new TimeSpan(14, 0, 0), Duration.Create(2, 0).Value)

            };
            await weeklyLogDbSet.AddRangeAsync(weeklyLogs);
            await context.SaveChangesAsync();

            var schedules = new Schedule[]
            {
                new Schedule(
                    null,
                    weeklyLogs[0].Id,
                    new DateTime(DateTime.Now.Year, 10, 18)
                ),
                new Schedule(
                    null,
                    weeklyLogs[6].Id,
                    new DateTime(DateTime.Now.Year, 10, 18)
                ),
                new Schedule(
                    null,
                    weeklyLogs[0].Id,
                    new DateTime(DateTime.Now.Year, 10, 15)
                ),
                new Schedule(
                    null,
                    weeklyLogs[7].Id,
                    new DateTime(DateTime.Now.Year, 10, 12)
                ),
                new Schedule(
                    null,
                    weeklyLogs[1].Id,
                    new DateTime(DateTime.Now.Year, 10, 22)
                ),
                new Schedule(
                    null,
                    weeklyLogs[7].Id,
                    new DateTime(DateTime.Now.Year, 10, 19)
                ),
                new Schedule(
                    null,
                    weeklyLogs[2].Id,
                    new DateTime(DateTime.Now.Year, 10, 22)
                ),
                new Schedule(
                    null,
                    weeklyLogs[5].Id,
                    new DateTime(DateTime.Now.Year, 10, 18)
                ),
                new Schedule(
                    null,
                    weeklyLogs[3].Id,
                    new DateTime(DateTime.Now.Year, 10, 20)
                ),
                new Schedule(
                    null,
                    weeklyLogs[4].Id,
                    new DateTime(DateTime.Now.Year, 10, 21)
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

        private static BeneficiaryWeeklyLog CreateWeeklyLogForBeneficiary(Guid beneficiaryId, string dayOfWeek, TimeSpan startTime,
                Duration duration)
        {
            DayOfWeek day = DayOfWeek.Create(dayOfWeek).Value;
            BeneficiaryWeeklyLog beneficiaryWeeklyLog = new BeneficiaryWeeklyLog(beneficiaryId, startTime, day, duration);

            return beneficiaryWeeklyLog;
        }
    }
}
