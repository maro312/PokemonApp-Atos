using Application.Contracts;
using Application.Dtos.Owners;
using Application.Mapping;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Pokemon.Core.Results;

namespace Application.Services.Owners;

public class OwnerAppService : IOwnerAppService
{
    private readonly IGenericRepository<Owner, int> _repository;

    public OwnerAppService(IGenericRepository<Owner, int> repository)
    {
        _repository = repository;
    }

    public async Task<Result<OwnerDto>> CreateAsync(CreateUpdateOwnerDto input)
    {
        Owner entity = input.ToEntity();
        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();
        OwnerDto dto = entity.ToDto();
        return Result<OwnerDto>.Success(dto);
    }

    public async Task<Result<OwnerDto>> DeleteAsync(int id)
    {
        Owner? entity = await _repository.GetByIdAsync(id);
        if (entity is null)
        {
            return Result<OwnerDto>.NotFound($"Owner with ID {id} not found.");
        }
        await _repository.DeleteAsync(entity);
        OwnerDto dto = entity.ToDto();
        return Result.Success(dto);
    }

    public async Task<Result<OwnerDto>> GetByIdAsync(int id)
    {
        IQueryable<Owner>? query = _repository.GetAllQuerable();
        Owner? entity = await query.AsNoTracking()
            .Where(x => x.Id == id)
            .Include(x => x.Country)
            .FirstOrDefaultAsync();
        if (entity is null)
        {
            return Result<OwnerDto>.NotFound($"Owner with ID {id} not found.");
        }
        OwnerDto dto = entity.ToDto();
        return Result<OwnerDto>.Success(dto);
    }
    public async Task<Result<List<OwnerDto>>> GetAllAsync()
    {
        IQueryable<Owner>? query = _repository.GetAllQuerable()
            .AsNoTracking();
        List<Owner> entities = (await query.Include(x => x.Country).ToListAsync());
        List<OwnerDto> dtos = entities.Select(e => e.ToDto()).ToList();
        return Result<List<OwnerDto>>.Success(dtos);
    }

    public async Task<Result<OwnerDto>> UpdateAsync(CreateUpdateOwnerDto input, int id)
    {
        Owner? entity = await _repository.GetByIdAsync(id);
        if (entity is null)
        {
            return Result<OwnerDto>.NotFound($"Owner with ID {id} not found.");
        }
        Owner? update = input.ToEntity(entity);
        await _repository.UpdateAsync(update);
        OwnerDto dto = update.ToDto();
        return Result.Success(dto);
    }
}
