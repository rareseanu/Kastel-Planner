using Application.RepositoryInterfaces;
using Domain.BeneficiaryWeeklyLogs;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class BeneficiaryWeeklyLogImpl : IBeneficiaryWeeklyLog
    {
        public KastelPlannerDbContext context;

        public BeneficiaryWeeklyLogImpl (KastelPlannerDbContext context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            BeneficiaryWeeklyLog beneficiaryWeeklyLog = context.BeneficiaryWeeklyLogs.Find(id);
            context.BeneficiaryWeeklyLogs.Remove(beneficiaryWeeklyLog);
        }

        public IEnumerable<BeneficiaryWeeklyLog> GetAll()
        {
            return context.BeneficiaryWeeklyLogs.ToList();
        }

        public BeneficiaryWeeklyLog GetById(int id)
        {
            return context.BeneficiaryWeeklyLogs.Find(id);
        }

        public void Insert(BeneficiaryWeeklyLog beneficiaryWeeklyLog)
        {
            context.BeneficiaryWeeklyLogs.Add(beneficiaryWeeklyLog);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(BeneficiaryWeeklyLog beneficiaryWeeklyLog)
        {
            context.Entry(beneficiaryWeeklyLog).State = EntityState.Modified;
        }
    }
}