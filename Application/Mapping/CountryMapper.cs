using Application.Dtos.Countries;
using Domain.Entities;

namespace Application.Mapping;

public static class CountryMapper
{
    public static CountryDto ToDto(this Country country)
    {
        if (country == null)
            return null;

        return new CountryDto
        {
            Id = country.Id,
            Name = country.Name
        };
    }

    public static Country ToEntity(this CreateUpdateCountryDto dto)
    {
        if (dto == null)
            return null;

        return new Country
        {
            Name = dto.Name
        };
    }

    public static Country ToEntity(this CreateUpdateCountryDto dto, Country existingCountry)
    {
        if (dto == null || existingCountry == null)
            return existingCountry;

        existingCountry.Name = dto.Name;
        return existingCountry;
    }
}
