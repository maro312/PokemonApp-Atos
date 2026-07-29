using Application.Dtos.Countries;
using Core.Dtos;

namespace Application.Dtos.Owners;

public class OwnerDto : BaseDto<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gym { get; set; }
    public int CountryId { get; set; }
    public CountryDto Country { get; set; }
}
