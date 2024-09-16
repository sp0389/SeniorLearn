namespace SeniorLearn.Data;

using SeniorLearn.Models.Enum;
using System.Net;

//Purpose: Represents a course that contains multiple lessons. Includes properties like Title, Description, StartDate, EndDate, and DeliveryType.


public class Course
{
    public int Id { get; private set; }
    public string Title { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public string MemberId { get; private set; } = default!;
    public Member Member { get; private set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DeliveryType Type { get; set; }
    public Availability Availability { get; set; } = Availability.Draft;

    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    public ICollection<CourseEnrolment> CourseEnrolments { get; private set; } = new List<CourseEnrolment>();

    public Course() { }

    public Course(string title, string description, Member member, DateTime startDate, DateTime endDate, DeliveryType type)
    {
        Title = title;
        Description = description;
        Member = member;
        MemberId = member.Id;
        StartDate = startDate;
        EndDate = endDate;
        Type = type;
    }
}