using Application.Contracts.Shared;
using Application.Dtos.Countries;
using Pokemon.Core.Results;

namespace Application.Contracts;

public interface ICountryAppService : ICrudAppService<CreateUpdateCountryDto, Result<CountryDto>, int, Result<List<CountryDto>>>
{
}
