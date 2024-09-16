using SeniorLearn.Models.Enum;

namespace SeniorLearn.Data

//Purpose: Represents a lesson entity, which can be standalone or part of a course.
//Includes properties like Title, Description, StartDate, EndDate, and Duration.

{
    public class Lesson
    {
        public int Id { get; private set; }
        public string Title { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public Member Member { get; private set; } = default!;
        public string MemberId { get; private set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Address { get; private set; } = default!;
        public int Duration { get; set; }
        public DeliveryType Type { get; set; }
        public Availability Availability { get; set; } = Availability.Draft;

        // Flag to indicate if the lesson is standalone or part of a course
        public bool IsStandalone { get; set; }
        public int? CourseId { get; set; }  // Nullable if not part of a course
        public Course? Course { get; set; }

        public ICollection<LessonEnrolment> LessonEnrolments { get; private set; } = new List<LessonEnrolment>();

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
