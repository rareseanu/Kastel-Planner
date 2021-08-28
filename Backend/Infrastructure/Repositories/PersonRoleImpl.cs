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
    public class PersonRoleImpl : IPersonRole
    {
        public KastelPlannerDbContext context;

        public PersonRoleImpl(KastelPlannerDbContext context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            PersonRole personRole = context.PersonRoles.Find(id);
            context.PersonRoles.Remove(personRole);
        }

        public IEnumerable<PersonRole> GetAll()
        {
            return context.PersonRoles.ToList();
        }

        public PersonRole GetById(int id)
        {
            return context.PersonRoles.Find(id);
        }

        public void Insert(PersonRole personRole)
        {
            context.PersonRoles.Add(personRole);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(PersonRole personRole)
        {
            context.Entry(personRole).State = EntityState.Modified;
        }
    }
}