using SeniorLearn.Models.Enum;

namespace SeniorLearn.Data
{
    // Purpose: Represents a lesson entity, which can be standalone or part of a course.
    // Includes properties like Title, Description, StartDate, EndDate, and Duration.
    public class Lesson
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Member Member { get; set; } = default!;
        public string MemberId { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Address { get; set; } = string.Empty;
        public int Duration { get; set; }
        public DeliveryType Type { get; set; }
        public Availability Availability { get; set; } = Availability.Draft;

        // Flag to indicate if the lesson is standalone or part of a course
        public bool IsStandalone { get; set; }

        // Course relationship properties
        public int? CourseId { get; set; }  // Nullable if not part of a course
        public Course? Course { get; set; }

        // Collection of lesson enrollments
        public ICollection<LessonEnrolment> LessonEnrolments { get; set; } = new List<LessonEnrolment>();

        // Default parameterless constructor for Entity Framework
        protected Lesson() { }

        // Constructor for a standalone lesson
        public Lesson(string title, string description, int duration, Member member, string address, DateTime startDate, DateTime endDate, DeliveryType type)
        {
            Title = title;
            Description = description;
            Duration = duration;
            Member = member;
            MemberId = member.Id;
            Address = address;
            StartDate = startDate;
            EndDate = endDate;
            Type = type;
            IsStandalone = true;
        }

        // Constructor for a lesson within a course
        public Lesson(string title, string description, int duration, Member member, Course course, string address, DateTime startDate, DateTime endDate, DeliveryType type)
            : this(title, description, duration, member, address, startDate, endDate, type)
        {
            Course = course;
            CourseId = course.Id;
            IsStandalone = false;
        }
    }
}
