using Application.Contracts;
using Application.Dtos.Categories;
using Application.Mapping;
using Domain.Entities;
using Domain.Repositories;
using Pokemon.Core.Results;

namespace Application.Services.Categories;

public class CategoryAppService : ICategoryAppService
{
    private readonly IGenericRepository<Category, int> _repository;

    public CategoryAppService(IGenericRepository<Category, int> repository)
    {
        _repository = repository;
    }

    public async Task<Result<CategoryDto>> CreateAsync(CreateUpdateCategoryDto input)
    {
        Category entity = input.ToEntity();
        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();
        CategoryDto dto = entity.ToDto();
        return Result<CategoryDto>.Success(dto);
    }

    public async Task<Result<CategoryDto>> DeleteAsync(int id)
    {
        Category? entity = await _repository.GetByIdAsync(id);
        if (entity is null)
        {
            return Result<CategoryDto>.NotFound($"Category with ID {id} not found.");
        }
        await _repository.DeleteAsync(entity);
        CategoryDto dto = entity.ToDto();
        return Result<CategoryDto>.Success(dto);
    }

    public async Task<Result<CategoryDto>> GetByIdAsync(int id)
    {
        Category? entity = await _repository.GetByIdAsNoTrackingAsync(id);
        if (entity is null)
        {
            return Result<CategoryDto>.NotFound($"Category with ID {id} not found.");
        }
        CategoryDto dto = entity.ToDto();
        return Result<CategoryDto>.Success(dto);
    }

    public async Task<Result<List<CategoryDto>>> GetAllAsync()
    {
        List<Category> entities = (await _repository.GetAllAsync()).ToList();
        List<CategoryDto> dtos = entities.Select(e => e.ToDto()).ToList();
        return Result<List<CategoryDto>>.Success(dtos);
    }

    public async Task<Result<CategoryDto>> UpdateAsync(CreateUpdateCategoryDto input, int id)
    {
        Category? entity = await _repository.GetByIdAsync(id);
        if (entity is null)
        {
            return Result<CategoryDto>.NotFound($"Category with ID {id} not found.");
        }
        Category? update = input.ToEntity(entity);
        await _repository.UpdateAsync(update);
        CategoryDto dto = update.ToDto();
        return Result<CategoryDto>.Success(dto);
    }
}
