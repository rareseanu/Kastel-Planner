using Domain.PersonsLabels;
using System.Collections.Generic;

namespace Application.RepositoryInterfaces
{
    public interface IPersonLabel
    {
        IEnumerable<PersonLabel> GetAll();
        PersonLabel GetById(int id);
        void Insert(PersonLabel personLabel);
        void Update(PersonLabel personLabel);
        void Delete(int id);
        void Save();
    }
}
