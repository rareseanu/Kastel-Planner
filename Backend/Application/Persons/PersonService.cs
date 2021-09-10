using Application.Persons.Requests;
using Application.Persons.Responses;
using Application.RepositoryInterfaces;
using Domain;
using Domain.Persons;
using Domain.Persons.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Persons
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private bool isActive;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
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

            var persons = await _personRepository.GetAllAsync();

            foreach (var person in persons)
            {
                var personResponse = new PersonResponse()
                {
                    Id = person.Id,
                    FirstName = person.Name.FirstName,
                    LastName = person.Name.LastName,
                    PhoneNumber = person.PhoneNumber.Number,
                    IsActive = person.IsActive
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
    }
}
