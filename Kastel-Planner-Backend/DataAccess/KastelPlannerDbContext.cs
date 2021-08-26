using Kastel_Planner_Backend.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Kastel_Planner_Backend.DataAccess
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