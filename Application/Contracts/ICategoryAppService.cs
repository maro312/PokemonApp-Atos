using Application.Contracts.Shared;
using Application.Dtos.Categories;
using Pokemon.Core.Results;

namespace Application.Contracts;

public interface ICategoryAppService : ICrudAppService<CreateUpdateCategoryDto, Result<CategoryDto>, int, Result<List<CategoryDto>>>
{
}
