using Domain.Persons;
using System.Collections.Generic;

namespace Application.RepositoryInterfaces
{
    public interface IPerson
    {
        IEnumerable<Person> GetAll();
        Person GetById(int id);
        void Insert(Person person);
        void Update(Person person);
        void Delete(int id);
        void Save();
    }
}
