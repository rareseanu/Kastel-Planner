using Domain.PersonsRoles;
using System.Collections.Generic;

namespace Application.RepositoryInterfaces
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
