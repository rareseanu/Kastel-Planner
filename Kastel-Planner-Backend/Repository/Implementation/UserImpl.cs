using Kastel_Planner_Backend.DataAccess;
using Kastel_Planner_Backend.Models;
using Kastel_Planner_Backend.Repository.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Kastel_Planner_Backend.Repository.Implementation
{
    public class UserImpl : IUser
    {
        public KastelPlannerDbContext context;

        public UserImpl(KastelPlannerDbContext context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            User user = context.Users.Find(id);
            context.Users.Remove(user);
        }

        public IEnumerable<User> GetAll()
        {
            return context.Users.ToList();
        }

        public User GetById(int id)
        {
            return context.Users.Find(id);
        }

        public void Insert(User user)
        {
            context.Users.Add(user);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(User user)
        {
            context.Entry(user).State = EntityState.Modified;
        }
    }
}