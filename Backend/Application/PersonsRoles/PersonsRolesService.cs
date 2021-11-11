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
    public class PersonsRolesService : IPersonsRolesService
    {
        private readonly IPersonRoleRepository _personRoleRepository;
        private readonly IPersonRepository _personRepository;
        private Guid roleId, personId;

        public PersonsRolesService(IPersonRoleRepository personRoleRepository, IPersonRepository personRepository)
        {
            _personRoleRepository = personRoleRepository;
            _personRepository = personRepository;
        }

        public async Task<Result<PersonsRolesResponse>> CreatePersonRoleAsync(CreatePersonsRolesRequest request)
        {
            roleId = request.RoleId;
            personId = request.PersonId;

            var person = await _personRepository.GetByIdAsync(request.PersonId);
            if(person == null)
            {
                return Result.Failure<PersonsRolesResponse>($"Person with Id {request.PersonId} was not found");
            }
            var oldPersonRole = await _personRoleRepository.GetFirstByPredicateAsync(pr => pr.PersonId == person.Id && pr.RoleId == request.RoleId);
            if(oldPersonRole != null)
            {
                var personRoleAlreadyExistingResponse = new PersonsRolesResponse()
                {

                    Id = oldPersonRole.Id,
                    RoleId = oldPersonRole.RoleId,
                    PersonId = oldPersonRole.PersonId
                };

                return Result.Success(personRoleAlreadyExistingResponse);
            }

            var personRole = new PersonRole(roleId, personId);

            await _personRoleRepository.AddAsync(personRole);

            var personRoleResponse = new PersonsRolesResponse()
            {

                Id = personRole.Id,
                RoleId = personRole.RoleId,
                PersonId = personRole.PersonId
            };

            return Result.Success(personRoleResponse);
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

        public async Task<Result> DeletePersonRolesAsync(RemoveRolesRequest request)
        {
            var personRoles = await _personRoleRepository.GetAllByPredicateAsync(p => p.PersonId.ToString().Equals(request.PersonId));

            foreach (var personRole in personRoles)
            {
                await _personRoleRepository.Delete(personRole);
            }
            return Result.Success();
        }

        public async Task<IList<PersonsRolesResponse>> GetAllPersonsRolesAsync()
        {
            var response = new List<PersonsRolesResponse>();

            var personsRoles = await _personRoleRepository.GetAllAsync();

            foreach (var personRole in personsRoles)
            {
                var personRoleResponse = new PersonsRolesResponse()
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

        public async Task<Result<PersonsRolesResponse>> UpdatePersonRoleAsync(Guid personRoleId, UpdatePersonsRolesRequest request)
        {
            roleId = request.RoleId;
            personId = request.PersonId;

            var personRole = await _personRoleRepository.GetByIdAsync(personRoleId);

            if (personRole == null)
            {
                return Result.Failure<PersonsRolesResponse>($"Person role with Id {personRoleId} was not found");
            }

            personRole.UpdatePersonRole(roleId, personId);

            await _personRoleRepository.Update(personRole);

            var response = new PersonsRolesResponse()
            {
                Id = personRole.Id,
                RoleId = personRole.RoleId,
                PersonId = personRole.PersonId
            };

            return Result.Success(response);
        }
    }
}
