using Kastel_Planner_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kastel_Planner_Backend.Repository.Interfaces
{
    public interface ISchedule
    {
        IEnumerable<Schedule> GetAll();
        Schedule GetById(int id);
        void Insert(Schedule schedule);
        void Update(Schedule schedule);
        void Delete(int id);
        void Save();
    }
}
