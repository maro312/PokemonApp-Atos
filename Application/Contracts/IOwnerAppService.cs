using Application.Contracts.Shared;
using Application.Dtos.Owners;
using Pokemon.Core.Results;

namespace Application.Contracts;

public interface IOwnerAppService : ICrudAppService<CreateUpdateOwnerDto, Result<OwnerDto>, int, Result<List<OwnerDto>>>
{
}
