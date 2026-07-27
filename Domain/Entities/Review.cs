using Core.Contracts;

namespace Domain.Entities;

public class Review : BaseEntity<int>
{
    public string Title { get; set; }
    public string Text { get; set; }
    public int Rating { get; set; }
    public Reviewer Reviewer { get; set; }
    public Pokemon Pokemon { get; set; }
}