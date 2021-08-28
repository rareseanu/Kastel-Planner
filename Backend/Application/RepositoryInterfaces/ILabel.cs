using System.Collections.Generic;
using System.Reflection.Emit;

namespace Application.RepositoryInterfaces
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
