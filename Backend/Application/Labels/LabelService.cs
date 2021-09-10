using Application.Labels.Requests;
using Application.Labels.Responses;
using Application.RepositoryInterfaces;
using Domain;
using Domain.Labels;
using Domain.Labels.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Labels
{
    public class LabelService : ILabelService
    {
        private readonly ILabelRepository _labelRepository;

        public LabelService(ILabelRepository labelRepository)
        {
            _labelRepository = labelRepository;
        }

        public async Task<Result<LabelResponse>> CreateLabelAsync(CreateLabelRequest request)
        {
            Result<LabelName> labelNameOrError = LabelName.Create(request?.LabelName);
            if (labelNameOrError.IsFailure)
            {
                return Result.Failure<LabelResponse>(labelNameOrError.Error);
            }

            var label = new Label (labelNameOrError.Value);

            await _labelRepository.AddAsync(label);

            var response = new LabelResponse()
            {
                Id = label.Id,
                LabelName = label.LabelName.Value
            };

            return Result.Success(response);
        }

        public async Task<Result> DeleteLabelAsync(Guid labelId)
        {
            var label = await _labelRepository.GetByIdAsync(labelId);

            if (label == null)
            {
                return Result.Failure($"Label with Id {labelId} was not found");
            }

            await _labelRepository.Delete(label);

            return Result.Success();
        }

        public async Task<IList<LabelResponse>> GetAllLabelsAsync()
        {
            var response = new List<LabelResponse>();

            var labels = await _labelRepository.GetAllAsync();

            foreach (var label in labels)
            {
                var labelResponse = new LabelResponse()
                {
                    Id = label.Id,
                    LabelName = label.LabelName.Value
                };

                response.Add(labelResponse);
            }

            return response;
        }

        public async Task<Result<LabelResponse>> GetLabelByAsync(Guid id)
        {
            var label = await _labelRepository.GetByIdAsync(id);

            if (label == null)
            {
                return Result.Failure<LabelResponse>($"Label with Id {id} was not found");
            }

            var response = new LabelResponse()
            {
                Id = label.Id,
                LabelName = label.LabelName.Value
            };

            return Result.Success(response);
        }

        public async Task<Result<LabelResponse>> UpdateLabelAsync(Guid labelId, UpdateLabelRequest request)
        {
            Result<LabelName> labelNameOrError = LabelName.Create(request?.LabelName);


            if (labelNameOrError.IsFailure)
            {
                return Result.Failure<LabelResponse>(labelNameOrError.Error);
            }

            var label = await _labelRepository.GetByIdAsync(labelId);

            if (label == null)
            {
                return Result.Failure<LabelResponse>($"Label with Id {labelId} was not found");
            }

            label.UpdateLabel(labelNameOrError.Value);

            await _labelRepository.Update(label);

            var response = new LabelResponse()
            {
                Id = label.Id,
                LabelName = label.LabelName.Value
            };

            return Result.Success(response);
        }
    }
}
