using Kastel_Planner_Backend.DataAccess;
using Kastel_Planner_Backend.Models;
using Kastel_Planner_Backend.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Kastel_Planner_Backend.Repository.Implementation
{
    public class PersonImpl : IPerson
    {
        public KastelPlannerDbContext context;

        public PersonImpl(KastelPlannerDbContext context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            Person person = context.Persons.Find(id);
            context.Persons.Remove(person);
        }

        public IEnumerable<Person> GetAll()
        {
            return context.Persons.ToList();
        }

        public Person GetById(int id)
        {
            return context.Persons.Find(id);
        }

        public void Insert(Person person)
        {
            context.Persons.Add(person);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Person person)
        {
            context.Entry(person).State = EntityState.Modified;
        }
    }
}