using Application.PersonsLabels.Requests;
using Application.PersonsLabels.Responses;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PersonsLabels
{
    public interface IPersonsLabelsService
    {
        Task<IList<PersonsLabelsResponse>> GetAllPersonsLabelsAsync();
        Task<Result<PersonsLabelsResponse>> GetPersonLabelByAsync(Guid id);
        Task<Result> CreatePersonLabelAsync(CreatePersonsLabelsRequest request);
        Task<Result> UpdatePersonLabelAsync(Guid personLabelId, UpdatePersonsLabelsRequest request);
        Task<Result> DeletePersonLabelAsync(Guid personLabelId);
    }
}
