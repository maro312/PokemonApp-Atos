using Application.Contracts.Shared;
using Application.Dtos.Pokemons;
using Pokemon.Core.Results;

namespace Application.Contracts;

public interface IPokemonAppService : ICrudAppService<CreateUpdatePokemonDto, Result<PokemonDto>, int, Result<List<PokemonDto>>>
{
}
