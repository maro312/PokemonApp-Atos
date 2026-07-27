using Core.Contracts;

namespace Domain.Entities;

public class Reviewer : BaseEntity<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<Review> Reviews { get; set; }
}