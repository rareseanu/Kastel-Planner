using Domain.Users;
using System.Collections.Generic;

namespace Application.RepositoryInterfaces
{
    public interface IUser
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Insert(User user);
        void Update(User user);
        void Delete(int id);
        void Save();
    }
}
