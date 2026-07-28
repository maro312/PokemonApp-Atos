using Application.Dtos.Owners;
using Domain.Entities;

namespace Application.Mapping;

public static class OwnerMapper
{
    public static OwnerDto ToDto(this Owner owner)
    {
        if (owner == null)
            return null;

        return new OwnerDto
        {
            Id = owner.Id,
            FirstName = owner.FirstName,
            LastName = owner.LastName,
            Gym = owner.Gym,
            CountryId = owner.CountryId,
            Country = owner.Country.ToDto()
        };
    }

    public static Owner ToEntity(this CreateUpdateOwnerDto dto)
    {
        if (dto == null)
            return null;

        return new Owner
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Gym = dto.Gym,
            CountryId = dto.CountryId
        };
    }

    public static Owner ToEntity(this CreateUpdateOwnerDto dto, Owner existingOwner)
    {
        if (dto == null || existingOwner == null)
            return existingOwner;

        existingOwner.FirstName = dto.FirstName;
        existingOwner.LastName = dto.LastName;
        existingOwner.Gym = dto.Gym;
        existingOwner.CountryId = dto.CountryId;
        
        return existingOwner;
    }
}
