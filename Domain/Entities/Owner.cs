using Core.Contracts;
using System.Diagnostics.Metrics;

namespace Domain.Entities;

public class Owner : BaseEntity<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gym { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; }
    public ICollection<PokemonOwner> PokemonOwners { get; set; }
}
