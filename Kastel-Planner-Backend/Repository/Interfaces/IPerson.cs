using Kastel_Planner_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kastel_Planner_Backend.Repository.Interfaces
{
    public interface IPerson
    {
        IEnumerable<Person> GetAll();
        Person GetById(int id);
        void Insert(Person person);
        void Update(Person person);
        void Delete(int id);
        void Save();
    }
}
