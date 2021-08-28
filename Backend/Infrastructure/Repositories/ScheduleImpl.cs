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
    public class ScheduleImpl : ISchedule
    {
        public KastelPlannerDbContext context;

        public ScheduleImpl (KastelPlannerDbContext context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            Schedule schedule = context.Schedules.Find(id);
            context.Schedules.Remove(schedule);
        }

        public IEnumerable<Schedule> GetAll()
        {
            return context.Schedules.ToList();
        }

        public Schedule GetById(int id)
        {
            return context.Schedules.Find(id);
        }

        public void Insert(Schedule schedule)
        {
            context.Schedules.Add(schedule);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Schedule schedule)
        {
            context.Entry(schedule).State = EntityState.Modified;
        }
    }
}