using Application.Contracts;
using Application.Dtos.Reviewers;
using Application.Mapping;
using Domain.Entities;
using Domain.Repositories;
using Pokemon.Core.Results;

namespace Application.Services.Reviewers;

public class ReviewerAppService : IReviewerAppService
{
    private readonly IGenericRepository<Reviewer, int> _repository;

    public ReviewerAppService(IGenericRepository<Reviewer, int> repository)
    {
        _repository = repository;
    }

    public async Task<Result<ReviewerDto>> CreateAsync(CreateUpdateReviewerDto input)
    {
        Reviewer entity = input.ToEntity();
        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();
        ReviewerDto dto = entity.ToDto();
        return Result<ReviewerDto>.Success(dto);
    }

    public async Task<Result<ReviewerDto>> DeleteAsync(int id)
    {
        Reviewer? entity = await _repository.GetByIdAsync(id);
        if (entity is null)
        {
            return Result<ReviewerDto>.NotFound($"Reviewer with ID {id} not found.");
        }
        await _repository.DeleteAsync(entity);
        ReviewerDto dto = entity.ToDto();
        return Result<ReviewerDto>.Success(dto);
    }

    public async Task<Result<ReviewerDto>> GetByIdAsync(int id)
    {
        Reviewer? entity = await _repository.GetByIdAsync(id);
        if (entity is null)
        {
            return Result<ReviewerDto>.NotFound($"Reviewer with ID {id} not found.");
        }
        ReviewerDto dto = entity.ToDto();
        return Result<ReviewerDto>.Success(dto);
    }

    public async Task<Result<List<ReviewerDto>>> GetAllAsync()
    {
        List<Reviewer> entities = (await _repository.GetAllAsync()).ToList();
        List<ReviewerDto> dtos = entities.Select(e => e.ToDto()).ToList();
        return Result<List<ReviewerDto>>.Success(dtos);
    }

    public async Task<Result<ReviewerDto>> UpdateAsync(CreateUpdateReviewerDto input, int id)
    {
        Reviewer? entity = await _repository.GetByIdAsync(id);
        if (entity is null)
        {
            return Result<ReviewerDto>.NotFound($"Reviewer with ID {id} not found.");
        }
        Reviewer? update = input.ToEntity(entity);
        await _repository.UpdateAsync(update);
        ReviewerDto dto = update.ToDto();
        return Result<ReviewerDto>.Success(dto);
    }
}
