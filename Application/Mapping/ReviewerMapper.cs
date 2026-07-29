using Application.Dtos.Reviewers;
using Domain.Entities;

namespace Application.Mapping;

public static class ReviewerMapper
{
    public static ReviewerDto ToDto(this Reviewer entity)
    {
        if (entity == null) return null;
        return new ReviewerDto
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName
        };
    }

    public static Reviewer ToEntity(this CreateUpdateReviewerDto dto)
    {
        if (dto == null) return null;
        return new Reviewer
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName
        };
    }

    public static Reviewer ToEntity(this CreateUpdateReviewerDto dto, Reviewer existingEntity)
    {
        if (dto == null || existingEntity == null) return existingEntity;
        
        existingEntity.FirstName = dto.FirstName;
        existingEntity.LastName = dto.LastName;
        
        return existingEntity;
    }
}
