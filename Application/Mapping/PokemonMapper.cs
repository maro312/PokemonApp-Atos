using Application.Dtos.Pokemons;
using Domain.Entities;
using PokemonEntity = Domain.Entities.Pokemon;

namespace Application.Mapping;

public static class PokemonMapper
{
    public static PokemonDto ToDto(this PokemonEntity entity)
    {
        if (entity == null) return null;
        return new PokemonDto
        {
            Id = entity.Id,
            Name = entity.Name,
            BirthDate = entity.BirthDate,
            Categories = entity.PokemonCategories?.Select(pc => pc.Category?.ToDto()).Where(c => c != null).ToList()
        };
    }

    public static PokemonEntity ToEntity(this CreateUpdatePokemonDto dto)
    {
        if (dto == null) return null;
        return new PokemonEntity
        {
            Name = dto.Name,
            BirthDate = dto.BirthDate
        };
    }

    public static PokemonEntity ToEntity(this CreateUpdatePokemonDto dto, PokemonEntity existingEntity)
    {
        if (dto == null || existingEntity == null) return existingEntity;
        
        existingEntity.Name = dto.Name;
        existingEntity.BirthDate = dto.BirthDate;
        
        return existingEntity;
    }
}
