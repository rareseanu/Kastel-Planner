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
    public class PersonLabelImpl : IPersonLabel
    {
        public KastelPlannerDbContext context;

        public PersonLabelImpl(KastelPlannerDbContext context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            PersonLabel personLabel = context.PersonLabels.Find(id);
            context.PersonLabels.Remove(personLabel);
        }

        public IEnumerable<PersonLabel> GetAll()
        {
            return context.PersonLabels.ToList();
        }

        public PersonLabel GetById(int id)
        {
            return context.PersonLabels.Find(id);
        }

        public void Insert(PersonLabel personLabel)
        {
            context.PersonLabels.Add(personLabel);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(PersonLabel personLabel)
        {
            context.Entry(personLabel).State = EntityState.Modified;
        }
    }
}