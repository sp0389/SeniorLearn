

namespace SeniorLearn.Data

//Purpose: Represents the base class for LessonEnrolment and CourseEnrolment.
//Each enrollment ties a member to a course or lesson with an enrollment date.

{
    public abstract class Enrolment
    {
        public int Id { get; private set; }
        public string MemberId { get; private set; } = default!;
        public Member Member { get; private set; } = default!;
        public DateTime EnrolmentDate { get; private set; }
        protected Enrolment() { }
        protected Enrolment(Member member, DateTime enrolmentDate)
        {
            Member = member;
            MemberId = member.Id;
            EnrolmentDate = enrolmentDate;
        }
    }

    public class CourseEnrolment : Enrolment
    {
        public int CourseId { get; private set; }
        public Course Course { get; private set; } = default!;

        public CourseEnrolment() { }

        public CourseEnrolment(Member member, Course course, DateTime enrolmentDate) : base(member, enrolmentDate)
        {
            Course = course;
            CourseId = course.Id;
        }
    }

    public class LessonEnrolment : Enrolment
    {
        public int LessonId { get; private set; }
        public Lesson Lesson { get; private set; } = default!;

        public LessonEnrolment() { }

        public LessonEnrolment(Member member, Lesson lesson, DateTime enrolmentDate) : base(member, enrolmentDate)
        {
            Lesson = lesson;
            LessonId = lesson.Id;
        }
    }

}
