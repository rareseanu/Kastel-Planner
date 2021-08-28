using Domain.BeneficiaryWeeklyLogs;
using Domain.Labels;
using Domain.Persons;
using Domain.PersonsLabels;
using Domain.PersonsRoles;
using Domain.Roles;
using Domain.Schedules;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class KastelPlannerDbContext : DbContext
    {
        public virtual DbSet<BeneficiaryWeeklyLog> BeneficiaryWeeklyLogs { get; set; }
        public virtual DbSet<Label> Labels { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<PersonLabel> PersonLabels { get; set; }
        public virtual DbSet<PersonRole> PersonRoles { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
