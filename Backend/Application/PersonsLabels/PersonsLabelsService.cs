using Application.PersonsLabels.Requests;
using Application.PersonsLabels.Responses;
using Application.PersonsRoles.Requests;
using Application.RepositoryInterfaces;
using Domain;
using Domain.PersonsLabels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.PersonsLabels
{
    public class PersonsLabelsService : IPersonsLabelsService
    {
        private readonly IPersonLabelRepository _personLabelRepository;
        private Guid labelId, personId;

        public PersonsLabelsService(IPersonLabelRepository personLabelRepository)
        {
            _personLabelRepository = personLabelRepository;
        }


        public async Task<Result<PersonsLabelsResponse>> CreatePersonLabelAsync(CreatePersonsLabelsRequest request)
        {
            labelId = request.LabelId;
            personId = request.PersonId;

            var personLabel = new PersonLabel(personId, labelId);

            await _personLabelRepository.AddAsync(personLabel);

            var personLabelResponse = new PersonsLabelsResponse()
            {

                Id = personLabel.Id,
                PersonId = personLabel.PersonId,
                LabelId = personLabel.LabelId
            };

            return Result.Success(personLabelResponse);
        }

        public async Task<Result> DeletePersonLabelAsync(Guid personLabelId)
        {
            var personLabel = await _personLabelRepository.GetByIdAsync(personLabelId);

            if (personLabel == null)
            {
                return Result.Failure($"Person label with Id {personLabelId} was not found");
            }

            await _personLabelRepository.Delete(personLabel);

            return Result.Success();
        }

        public async Task<Result> DeletePersonLabels(RemoveLabelsRequest request)
        {
            var personLabels = await _personLabelRepository.GetAllByPredicateAsync(p => p.PersonId.ToString().Equals(request.PersonId), p => p.Person);

            foreach (var personRole in personLabels)
            {
                await _personLabelRepository.Delete(personRole);
            }
            return Result.Success();
        }

        public async Task<IList<PersonsLabelsResponse>> GetAllPersonsLabelsAsync()
        {
            var response = new List<PersonsLabelsResponse>();

            var personsLabels = await _personLabelRepository.GetAllAsync();

            foreach (var personLabel in personsLabels)
            {
                var personLabelResponse = new PersonsLabelsResponse()
                {

                    Id = personLabel.Id,
                    PersonId = personLabel.PersonId,
                    LabelId = personLabel.LabelId
                };

                response.Add(personLabelResponse);
            }

            return response;
        }

        public async Task<Result<PersonsLabelsResponse>> GetPersonLabelByAsync(Guid id)
        {
            var personLabel = await _personLabelRepository.GetByIdAsync(id);

            if (personLabel == null)
            {
                return Result.Failure<PersonsLabelsResponse>($"PErson label with Id {id} was not found");
            }

            var response = new PersonsLabelsResponse()
            {
                Id = personLabel.Id,
                PersonId = personLabel.PersonId,
                LabelId = personLabel.LabelId
            };

            return Result.Success(response);
        }

        public async Task<Result<PersonsLabelsResponse>> UpdatePersonLabelAsync(Guid personLabelId, UpdatePersonsLabelsRequest request)
        {
            labelId = request.LabelId;
            personId = request.PersonId;

            var personLabel = await _personLabelRepository.GetByIdAsync(personLabelId);

            if (personLabel == null)
            {
                return Result.Failure<PersonsLabelsResponse>($"Person label with Id {personLabelId} was not found");
            }

            personLabel.UpdatePersonLabel(labelId, personId);

            await _personLabelRepository.Update(personLabel);

            var response = new PersonsLabelsResponse()
            {
                Id = personLabel.Id,
                PersonId = personLabel.PersonId,
                LabelId = personLabel.LabelId
            };

            return Result.Success(response);

        }
    }
}
