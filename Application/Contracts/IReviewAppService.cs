using Application.Contracts.Shared;
using Application.Dtos.Reviews;
using Pokemon.Core.Results;

namespace Application.Contracts;

public interface IReviewAppService : ICrudAppService<CreateUpdateReviewDto, Result<ReviewDto>, int, Result<List<ReviewDto>>>
{
}
