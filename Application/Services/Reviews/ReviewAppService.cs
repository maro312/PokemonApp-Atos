using Application.Contracts;
using Application.Dtos.Reviews;
using Application.Mapping;
using Domain.Entities;
using Domain.Repositories;
using Pokemon.Core.Results;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Reviews;

public class ReviewAppService : IReviewAppService
{
    private readonly IGenericRepository<Review, int> _repository;

    public ReviewAppService(IGenericRepository<Review, int> repository)
    {
        _repository = repository;
    }

    public async Task<Result<ReviewDto>> CreateAsync(CreateUpdateReviewDto input)
    {
        Review entity = input.ToEntity();
        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();
        ReviewDto dto = entity.ToDto();
        return Result<ReviewDto>.Success(dto);
    }

    public async Task<Result<ReviewDto>> DeleteAsync(int id)
    {
        Review? entity = await _repository.GetByIdAsync(id);
        if (entity is null)
        {
            return Result<ReviewDto>.NotFound($"Review with ID {id} not found.");
        }
        await _repository.DeleteAsync(entity);
        ReviewDto dto = entity.ToDto();
        return Result<ReviewDto>.Success(dto);
    }

    public async Task<Result<ReviewDto>> GetByIdAsync(int id)
    {
        Review? entity = await _repository.GetAllQuerable().Include(r => r.Reviewer).FirstOrDefaultAsync(r => r.Id == id);
        if (entity is null)
        {
            return Result<ReviewDto>.NotFound($"Review with ID {id} not found.");
        }
        ReviewDto dto = entity.ToDto();
        return Result<ReviewDto>.Success(dto);
    }

    public async Task<Result<List<ReviewDto>>> GetAllAsync()
    {
        List<Review> entities = await _repository.GetAllQuerable().Include(r => r.Reviewer).ToListAsync();
        List<ReviewDto> dtos = entities.Select(e => e.ToDto()).ToList();
        return Result<List<ReviewDto>>.Success(dtos);
    }

    public async Task<Result<ReviewDto>> UpdateAsync(CreateUpdateReviewDto input, int id)
    {
        Review? entity = await _repository.GetByIdAsync(id);
        if (entity is null)
        {
            return Result<ReviewDto>.NotFound($"Review with ID {id} not found.");
        }
        Review? update = input.ToEntity(entity);
        await _repository.UpdateAsync(update);
        ReviewDto dto = update.ToDto();
        return Result<ReviewDto>.Success(dto);
    }
}
