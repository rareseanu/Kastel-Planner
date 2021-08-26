using Kastel_Planner_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kastel_Planner_Backend.Repository.Interfaces
{
    public interface IBeneficiaryWeeklyLog
    {
        IEnumerable<BeneficiaryWeeklyLog> GetAll();
        BeneficiaryWeeklyLog GetById (int id);
        void Insert(BeneficiaryWeeklyLog beneficiaryWeeklyLog);
        void Update(BeneficiaryWeeklyLog beneficiaryWeeklyLog);
        void Delete(int id);
        void Save();


    }
}
