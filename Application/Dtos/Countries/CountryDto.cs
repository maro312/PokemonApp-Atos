using Core.Dtos;

namespace Application.Dtos.Countries;

public class CountryDto : BaseDto<int>
{
    public string Name { get; set; }
}
