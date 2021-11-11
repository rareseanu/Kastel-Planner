using Application.Labels.Responses;
using Application.Persons.Requests;
using Application.Persons.Responses;
using Application.RepositoryInterfaces;
using Application.Roles.Responses;
using Domain;
using Domain.Persons;
using Domain.Persons.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Persons
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IPersonRoleRepository _roleRepository;
        private readonly IPersonLabelRepository _labelRepository;
        private bool isActive;

        public PersonService(IPersonRepository personRepository, IPersonRoleRepository roleRepository, IPersonLabelRepository labelRepository)
        {
            _personRepository = personRepository;
            _roleRepository = roleRepository;
            _labelRepository = labelRepository;
        }

        public async Task<Result<PersonResponse>> CreatePersonAsync(CreatePersonRequest request)
        {
            Result<Name>nameOrError = Name.Create(request?.FirstName, request?.LastName);
            Result<PhoneNumber> phoneNumberOrError = PhoneNumber.Create(request?.PhoneNumber);
            isActive = request.IsActive;

            if (nameOrError.IsFailure)
            {
                return Result.Failure<PersonResponse>(nameOrError.Error);
            }

            if (phoneNumberOrError.IsFailure)
            {
                return Result.Failure<PersonResponse>(phoneNumberOrError.Error);
            }

            var person = new Person(nameOrError.Value, phoneNumberOrError.Value, isActive);

            await _personRepository.AddAsync(person);

            var personResponse = new PersonResponse()
            {
                Id = person.Id,
                FirstName = person.Name.FirstName,
                LastName = person.Name.LastName,
                PhoneNumber = person.PhoneNumber.Number,
                IsActive = person.IsActive
            };

            return Result.Success(personResponse);
        }

        public async Task<Result> DeletePersonAsync(Guid personId)
        {
            var person = await _personRepository.GetByIdAsync(personId);

            if (person == null)
            {
                return Result.Failure($"Person with Id {personId} was not found");
            }

            await _personRepository.Delete(person);

            return Result.Success();
        }

        public async Task<IList<PersonResponse>> GetAllPersonsAsync()
        {
            var response = new List<PersonResponse>();

            var persons = await _personRepository.GetAllAsync(p => p.User, p => p.PersonRoles, p => p.PersonLabels);

            foreach (var person in persons)
            {
                var roles = await _roleRepository.GetAllByPredicateAsync(r => r.PersonId.Equals(person.Id), r => r.Role);
                var labels = await _labelRepository.GetAllByPredicateAsync(l => l.PersonId.Equals(person.Id), l => l.Label);

                var personResponse = new PersonResponse()
                {
                    Id = person.Id,
                    FirstName = person.Name.FirstName,
                    LastName = person.Name.LastName,
                    PhoneNumber = person.PhoneNumber.Number,
                    IsActive = person.IsActive,
                    Roles = roles.Select(r => new RoleResponse() {
                        Id = r.Role.Id,
                        RoleName = r.Role.RoleName.Value
                    }).ToArray(),
                    Labels = labels.Select(r => new LabelResponse()
                    {
                        Id = r.Label.Id,
                        LabelName = r.Label.LabelName.Value
                    }).ToArray(),

                };

                response.Add(personResponse);
            }

            return response;
        }

        public async Task<Result<PersonResponse>> GetPersonByAsync(Guid id)
        {
            var person = await _personRepository.GetByIdAsync(id);

            if (person == null)
            {
                return Result.Failure<PersonResponse>($"Person with Id {id} was not found");
            }

            var roles = await _roleRepository.GetAllByPredicateAsync(r => r.PersonId.Equals(person.Id), r => r.Role);
            var labels = await _labelRepository.GetAllByPredicateAsync(l => l.PersonId.Equals(person.Id), l => l.Label);

            var response = new PersonResponse()
            {
                Id = person.Id,
                FirstName = person.Name.FirstName,
                LastName = person.Name.LastName,
                PhoneNumber = person.PhoneNumber.Number,
                IsActive = person.IsActive,
                Roles = roles.Select(r => new RoleResponse()
                {
                    Id = r.Role.Id,
                    RoleName = r.Role.RoleName.Value
                }).ToArray(),
                Labels = labels.Select(r => new LabelResponse()
                {
                    Id = r.Label.Id,
                    LabelName = r.Label.LabelName.Value
                }).ToArray(),
            };

            return Result.Success(response);
        }

        public async Task<Result<PersonResponse>> UpdatePersonAsync(Guid personId, UpdatePersonRequest request)
        {
            Result<Name> nameOrError = Name.Create(request?.FirstName, request?.LastName);
            Result<PhoneNumber> phoneNumberOrError = PhoneNumber.Create(request?.PhoneNumber);
            isActive = request.IsActive;

            if (nameOrError.IsFailure)
            {
                return Result.Failure<PersonResponse> (nameOrError.Error);
            }

            if (phoneNumberOrError.IsFailure)
            {
                return Result.Failure<PersonResponse>(phoneNumberOrError.Error);
            }

            var person = await _personRepository.GetByIdAsync(personId);

            if (person == null)
            {
                return Result.Failure<PersonResponse>($"Person with Id {personId} was not found");
            }

            person.UpdatePerson(nameOrError.Value, phoneNumberOrError.Value, request.IsActive);

            await _personRepository.Update(person);

            var response = new PersonResponse()
            {
                Id = person.Id,
                FirstName = person.Name.FirstName,
                LastName = person.Name.LastName,
                PhoneNumber = person.PhoneNumber.Number,
                IsActive = person.IsActive
            };

            return Result.Success(response);
        }

        public async Task<Result<PersonResponse>> Anonymize(AnonymizeRequest request)
        {
            var user = await _personRepository.GetByIdAsync(request.PersonId);
            const string defaultPhoneValue = "0000000000";

            if (user != null)
            {
                foreach (string field in request.Fields)
                {
                    if(field.Equals("PhoneNumber"))
                    {
                        user.PhoneNumber = PhoneNumber.Create(defaultPhoneValue).Value;
                    }
                }
            }

            var response = new PersonResponse()
            {
                Id = user.Id,
                FirstName = user.Name.FirstName,
                LastName = user.Name.LastName,
                PhoneNumber = user.PhoneNumber.Number,
                IsActive = user.IsActive
            };

            await _personRepository.Update(user);

            return Result.Success(response);
        }
    }
}
