using System.Collections.Generic;
using System.Linq;

namespace Kastel_Planner_Backend.Repository.Implementation
{
    public class LabelImpl : ILabel
    {
        public KastelPlannerDbContext context;

        public LabelImpl(KastelPlannerDbContext context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            Label label = context.Labels.Find(id);
            context.Labels.Remove(label);
        }

        public IEnumerable<Label> GetAll()
        {
            return context.Labels.ToList();
        }

        public Label GetById(int id)
        {
            return context.Labels.Find(id);
        }

        public void Insert(Label label)
        {
            context.Labels.Add(label);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Label label)
        {
            context.Entry(label).State = EntityState.Modified;
        }
    }
}