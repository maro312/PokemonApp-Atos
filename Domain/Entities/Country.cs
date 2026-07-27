using Core.Contracts;

namespace Domain.Entities;

public class Country : BaseEntity<int>
{
    public string Name { get; set; }
    public ICollection<Owner> Owners { get; set; }
}
