using SeniorLearn.Data.Core;
using System.ComponentModel.DataAnnotations;

namespace SeniorLearn.Data
{
    public enum DeliveryType
    {
        Virtual,
        [Display(Name = "On Campus")]
        OnCampus
    }

    public enum Availability
    {
        Draft,
        Scheduled,
        Closed,
        Complete,
        Cancelled
    }

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
        public Guid GroupId { get; set; }
        public bool IsInCourse { get; set; }
        public int? CourseId { get; set; } 
        public Course? Course { get; set; }
        public ICollection<Enrolment> Enrolments { get; set; } = new List<Enrolment>();
        private Lesson() { }

        public Lesson(string title, string description, int duration, Member member, string address, DateTime startDate, DateTime endDate, DeliveryType type, bool isInCourse, int? courseId, Guid groupId)
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
            IsInCourse = isInCourse;
            CourseId = courseId;
            GroupId = groupId;
        }

        public Enrolment EnrolMemberInLesson(Member member, Lesson lesson, DateTime enrolmentDate)
        {
            var enrol = new Enrolment(member, lesson, lesson.CourseId, enrolmentDate);
            return enrol;
        }
        public void EnrolmentValidationChecks(Member member, Lesson lesson)
        {
            if (member.Id == lesson.MemberId)
            {
                throw new DomainRuleException("You cannot enrol in a lesson you created.");
            }
        }
    }
}
