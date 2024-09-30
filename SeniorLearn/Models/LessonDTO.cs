using SeniorLearn.Data;

namespace SeniorLearn.Models;

public class LessonDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Member Member { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Address { get; set; } = default!;
    public int Duration { get; set; }
    public bool IsInCourse { get; set; }
    public Course? Course { get; set; }
    public Guid GroupId { get; set; }
}