using Application.PersonsRoles.Requests;
using Application.PersonsRoles.Responses;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PersonsRoles
{
    public interface IPersonsRolesService
    {
        Task<IList<PersonsRolesResponse>> GetAllPersonsRolesAsync();
        Task<Result<PersonsRolesResponse>> GetPersonRolesByAsync(Guid id);
        Task<Result<PersonsRolesResponse>> CreatePersonRoleAsync(CreatePersonsRolesRequest request);
        Task<Result<PersonsRolesResponse>> UpdatePersonRoleAsync(Guid personRoleId, UpdatePersonsRolesRequest request);
        Task<Result> DeletePersonRoleAsync(Guid personRoleId);
        Task<Result> DeletePersonRolesAsync(RemoveRolesRequest request);
    }
}
