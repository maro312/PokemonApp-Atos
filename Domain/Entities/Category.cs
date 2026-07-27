using Core.Contracts;

namespace Domain.Entities;

public class Category : BaseEntity<int>
{
    public string Name { get; set; }
    public ICollection<PokemonCategory> PokemonCategories { get; set; }
}
