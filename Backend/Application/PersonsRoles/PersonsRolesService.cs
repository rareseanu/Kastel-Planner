using Application.PersonsRoles.Requests;
using Application.PersonsRoles.Responses;
using Application.RepositoryInterfaces;
using Domain;
using Domain.PersonsRoles;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.PersonsRoles
{
    public sealed class PersonsRolesService : IPersonsRolesService
    {
        private readonly IPersonRoleRepository _personRoleRepository;
        private Guid roleId, personId;

        public PersonsRolesService(IPersonRoleRepository personRoleRepository)
        {
            _personRoleRepository = personRoleRepository;
        }

        public async Task<Result> CreatePersonRoleAsync(CreatePersonsRolesRequest request)
        {
            roleId = request.RoleId;
            personId = request.PersonId;

            var personRole = new PersonRole(roleId, personId);

            await _personRoleRepository.AddAsync(personRole);

            return Result.Success();
        }

        public async Task<Result> DeletePersonRoleAsync(Guid personRoleId)
        {
            var personRole = await _personRoleRepository.GetByIdAsync(personRoleId);

            if (personRole == null)
            {
                return Result.Failure($"Person role with Id {personRoleId} was not found");
            }

            await _personRoleRepository.Delete(personRole);

            return Result.Success();
        }

        public async Task<IList<PersonsRolesResponse>> GetAllPersonsRolesAsync()
        {
            var response = new List<PersonsRolesResponse>();

            var personsRoles = await _personRoleRepository.GetAllAsync();

            foreach (var personRole in personsRoles)
            {
                var personRoleResponse = new PersonsRolesResponse
                {

                    Id = personRole.Id,
                    RoleId = personRole.RoleId,
                    PersonId = personRole.PersonId           
                };

                response.Add(personRoleResponse);
            }

            return response;
        }

        public async Task<Result<PersonsRolesResponse>> GetPersonRolesByAsync(Guid id)
        {
            var personRole = await _personRoleRepository.GetByIdAsync(id);

            if (personRole == null)
            {
                return Result.Failure<PersonsRolesResponse>($"PErson role with Id {id} was not found");
            }

            var response = new PersonsRolesResponse()
            {
                Id = personRole.Id,
                RoleId = personRole.RoleId,
                PersonId = personRole.PersonId
            };

            return Result.Success(response);
        }

        public async Task<Result> UpdatePersonRoleAsync(Guid personRoleId, UpdatePersonsRolesRequest request)
        {
            roleId = request.RoleId;
            personId = request.PersonId;

            var personRole = await _personRoleRepository.GetByIdAsync(personRoleId);

            if (personRole == null)
            {
                return Result.Failure($"Person role with Id {personRoleId} was not found");
            }

            personRole.UpdatePersonRole(roleId, personId);

            await _personRoleRepository.Update(personRole);

            return Result.Success();
        }
    }
}
