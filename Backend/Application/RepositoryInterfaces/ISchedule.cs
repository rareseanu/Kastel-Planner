using Domain.Schedules;
using System.Collections.Generic;

namespace Application.RepositoryInterfaces
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
