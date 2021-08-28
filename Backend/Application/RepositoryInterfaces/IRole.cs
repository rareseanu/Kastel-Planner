using Domain.Roles;
using System.Collections.Generic;

namespace Application.RepositoryInterfaces
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
