using Application.Persons.Requests;
using Application.Persons.Responses;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persons
{
    public interface IPersonService
    {
        Task<IList<PersonResponse>> GetAllPersonsAsync();
        Task<Result<PersonResponse>> GetPersonByAsync(Guid id);
        Task<Result<PersonResponse>> CreatePersonAsync(CreatePersonRequest request);
        Task<Result<PersonResponse>> UpdatePersonAsync(Guid personId, UpdatePersonRequest request);
        Task<Result> DeletePersonAsync(Guid personId);
    }
}
