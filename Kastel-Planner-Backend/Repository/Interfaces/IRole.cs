using Kastel_Planner_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kastel_Planner_Backend.Repository.Interfaces
{
    public interface IRole
    {
        IEnumerable<Role> GetAll();
        Role GetById(int id);
        void Insert(Role role);
        void Update(Role role);
        void Delete(int id);
        void Save();
    }
}
