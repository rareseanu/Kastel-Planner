using Kastel_Planner_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kastel_Planner_Backend.Repository.Interfaces
{
    public interface IPersonRole
    {
        IEnumerable<PersonRole> GetAll();
        PersonRole GetById(int id);
        void Insert(PersonRole personRole);
        void Update(PersonRole personRole);
        void Delete(int id);
        void Save();
    }
}
