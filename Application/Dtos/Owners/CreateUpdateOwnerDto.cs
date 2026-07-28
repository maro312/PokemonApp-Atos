namespace Application.Dtos.Owners;

public class CreateUpdateOwnerDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gym { get; set; }
    public int CountryId { get; set; }
}
