using Application.Contracts;
using Application.Dtos.Pokemons;
using Application.Mapping;
using Domain.Entities;
using PokemonEntity = Domain.Entities.Pokemon;
using Domain.Repositories;
using Pokemon.Core.Results;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Pokemons;

public class PokemonAppService : IPokemonAppService
{
    private readonly IGenericRepository<PokemonEntity, int> _repository;

    public PokemonAppService(IGenericRepository<PokemonEntity, int> repository)
    {
        _repository = repository;
    }

    public async Task<Result<PokemonDto>> CreateAsync(CreateUpdatePokemonDto input)
    {
        PokemonEntity entity = input.ToEntity();
        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();
        PokemonDto dto = entity.ToDto();
        return Result<PokemonDto>.Success(dto);
    }

    public async Task<Result<PokemonDto>> DeleteAsync(int id)
    {
        PokemonEntity? entity = await _repository.GetByIdAsync(id);
        if (entity is null)
        {
            return Result<PokemonDto>.NotFound($"Pokemon with ID {id} not found.");
        }
        await _repository.DeleteAsync(entity);
        PokemonDto dto = entity.ToDto();
        return Result<PokemonDto>.Success(dto);
    }

    public async Task<Result<PokemonDto>> GetByIdAsync(int id)
    {
        PokemonEntity? entity = await _repository.GetAllQuerable()
            .AsNoTracking()
            .Include(p => p.PokemonCategories)
            .ThenInclude(pc => pc.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (entity is null)
        {
            return Result<PokemonDto>.NotFound($"Pokemon with ID {id} not found.");
        }
        PokemonDto dto = entity.ToDto();
        return Result<PokemonDto>.Success(dto);
    }

    public async Task<Result<List<PokemonDto>>> GetAllAsync()
    {
        List<PokemonEntity> entities = await _repository.GetAllQuerable()
            .AsNoTracking()
            .Include(p => p.PokemonCategories)
            .ThenInclude(pc => pc.Category)
            .ToListAsync();
        List<PokemonDto> dtos = entities.Select(e => e.ToDto()).ToList();
        return Result<List<PokemonDto>>.Success(dtos);
    }

    public async Task<Result<PokemonDto>> UpdateAsync(CreateUpdatePokemonDto input, int id)
    {
        PokemonEntity? entity = await _repository.GetByIdAsync(id);
        if (entity is null)
        {
            return Result<PokemonDto>.NotFound($"Pokemon with ID {id} not found.");
        }
        PokemonEntity? update = input.ToEntity(entity);
        await _repository.UpdateAsync(update);
        PokemonDto dto = update.ToDto();
        return Result<PokemonDto>.Success(dto);
    }
}
