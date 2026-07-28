using Application.Contracts;
using Application.Dtos.Countries;
using Application.Mapping;
using Domain.Entities;
using Domain.Repositories;
using Pokemon.Core.Results;

namespace Application.Services.Countries;

public class CountryAppService : ICountryAppService
{
    private readonly IGenericRepository<Country, int> _repository;

    public CountryAppService(IGenericRepository<Country, int> repository)
    {
        _repository = repository;
    }

    public async Task<Result<CountryDto>> CreateAsync(CreateUpdateCountryDto input)
    {
        Country entity = input.ToEntity();
        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();
        CountryDto dto = entity.ToDto();
        return Result<CountryDto>.Success(dto);
    }

    public async Task<Result<CountryDto>> DeleteAsync(int id)
    {
        Country? entity = await _repository.GetByIdAsync(id);
        if (entity is null)
        {
            return Result<CountryDto>.NotFound($"Country with ID {id} not found.");
        }
        await _repository.DeleteAsync(entity);
        CountryDto dto = entity.ToDto();
        return Result.Success(dto);
    }

    public async Task<Result<CountryDto>> GetByIdAsync(int id)
    {
        Country? entity = await _repository.GetByIdAsync(id);
        if (entity is null)
        {
            return Result<CountryDto>.NotFound($"Country with ID {id} not found.");
        }
        CountryDto dto = entity.ToDto();
        return Result<CountryDto>.Success(dto);

    }

    public async Task<Result<List<CountryDto>>> GetAllAsync()
    {
        List<Country> entities = (await _repository.GetAllAsync()).ToList();
        List<CountryDto> dtos = entities.Select(e => e.ToDto()).ToList();
        return Result<List<CountryDto>>.Success(dtos);
    }

    public async Task<Result<CountryDto>> UpdateAsync(CreateUpdateCountryDto input, int id)
    {
        Country? entity = await _repository.GetByIdAsync(id);
        if (entity is null)
        {
            return Result<CountryDto>.NotFound($"Country with ID {id} not found.");
        }
        Country? update = input.ToEntity(entity);
        await _repository.UpdateAsync(update);
        CountryDto dto = update.ToDto();
        return Result.Success(dto);
    }
}
