using Application.Labels.Requests;
using Application.Labels.Responses;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Labels
{
    public interface ILabelService
    {
        Task<IList<LabelResponse>> GetAllLabelsAsync();
        Task<Result<LabelResponse>> GetLabelByAsync(Guid id);
        Task<Result<LabelResponse>> CreateLabelAsync(CreateLabelRequest request);
        Task<Result<LabelResponse>> UpdateLabelAsync(Guid labelId, UpdateLabelRequest request);
        Task<Result> DeleteLabelAsync(Guid labelId);
    }
}
