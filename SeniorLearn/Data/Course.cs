using SeniorLearn.Data;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}