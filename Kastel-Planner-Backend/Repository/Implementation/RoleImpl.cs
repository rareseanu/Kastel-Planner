using Kastel_Planner_Backend.DataAccess;
using Kastel_Planner_Backend.Models;
using Kastel_Planner_Backend.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Kastel_Planner_Backend.Repository.Implementation
{
    public class RoleImpl : IRole
    {
        public KastelPlannerDbContext context;

        public RoleImpl(KastelPlannerDbContext context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            Role role = context.Roles.Find(id);
            context.Roles.Remove(role);
        }

        public IEnumerable<Role> GetAll()
        {
            return context.Roles.ToList();
        }

        public Role GetById(int id)
        {
            return context.Roles.Find(id);
        }

        public void Insert(Role role)
        {
            context.Roles.Add(role);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Role role)
        {
            context.Entry(role).State = EntityState.Modified;
        }
    }
}