using SeniorLearn.Data;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string UserId { get; set; } = string.Empty; // Updated from InstructorId to UserId

    // New property to establish the relationship with Lesson
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
