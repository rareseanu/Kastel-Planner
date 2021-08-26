using Kastel_Planner_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kastel_Planner_Backend.Repository.Interfaces
{
    public interface ILabel 
    {
        IEnumerable<Label> GetAll();
        Label GetById(int id);
        void Insert(Label label);
        void Update(Label label);
        void Delete(int id);
        void Save();
    }
}
