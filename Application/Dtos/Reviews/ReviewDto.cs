namespace Application.Dtos.Reviews;

public class ReviewDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public int Rating { get; set; }
    
    public Application.Dtos.Reviewers.ReviewerDto Reviewer { get; set; }
}
