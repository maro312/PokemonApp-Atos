using Application.Dtos.Categories;
using Domain.Entities;

namespace Application.Mapping;

public static class CategoryMapper
{
    public static CategoryDto ToDto(this Category entity)
    {
        if (entity == null) return null;
        return new CategoryDto
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }

    public static Category ToEntity(this CreateUpdateCategoryDto dto)
    {
        if (dto == null) return null;
        return new Category
        {
            Name = dto.Name
        };
    }

    public static Category ToEntity(this CreateUpdateCategoryDto dto, Category existingEntity)
    {
        if (dto == null || existingEntity == null) return existingEntity;
        
        existingEntity.Name = dto.Name;
        
        return existingEntity;
    }
}
