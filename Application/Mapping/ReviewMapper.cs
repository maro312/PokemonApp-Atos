using Application.Dtos.Reviews;
using Domain.Entities;

namespace Application.Mapping;

public static class ReviewMapper
{
    public static ReviewDto ToDto(this Review entity)
    {
        if (entity == null) return null;
        return new ReviewDto
        {
            Id = entity.Id,
            Title = entity.Title,
            Text = entity.Text,
            Rating = entity.Rating,
            Reviewer = entity.Reviewer?.ToDto()
        };
    }

    public static Review ToEntity(this CreateUpdateReviewDto dto)
    {
        if (dto == null) return null;
        return new Review
        {
            Title = dto.Title,
            Text = dto.Text,
            Rating = dto.Rating
        };
    }

    public static Review ToEntity(this CreateUpdateReviewDto dto, Review existingEntity)
    {
        if (dto == null || existingEntity == null) return existingEntity;
        
        existingEntity.Title = dto.Title;
        existingEntity.Text = dto.Text;
        existingEntity.Rating = dto.Rating;
        
        return existingEntity;
    }
}
