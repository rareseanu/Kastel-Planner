using Kastel_Planner_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kastel_Planner_Backend.Repository.Interfaces
{
    public interface IPersonLabel
    {
        IEnumerable<PersonLabel> GetAll();
        PersonLabel GetById(int id);
        void Insert(PersonLabel personLabel);
        void Update(PersonLabel personLabel);
        void Delete(int id);
        void Save();
    }
}
