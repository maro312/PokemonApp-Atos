using Application.Contracts.Shared;
using Application.Dtos.Reviewers;
using Pokemon.Core.Results;

namespace Application.Contracts;

public interface IReviewerAppService : ICrudAppService<CreateUpdateReviewerDto, Result<ReviewerDto>, int, Result<List<ReviewerDto>>>
{
}
