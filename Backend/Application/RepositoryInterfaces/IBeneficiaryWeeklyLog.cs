using Domain.BeneficiaryWeeklyLogs;
using System.Collections.Generic;

namespace Application.RepositoryInterfaces
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
