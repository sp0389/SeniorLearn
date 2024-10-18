namespace SeniorLearn.Data
{
    public class Enrolment
    {
        public int Id { get; private set; }
        public string MemberId { get; private set; } = default!;
        public Member Member { get; private set; } = default!;
        public int LessonId { get; private set; }
        public Lesson Lesson { get; private set; } = default!;
        public int? CourseId { get; private set; }
        public Course? Course { get; private set; } = default!;
        public DateTime EnrolmentDate { get; private set; }

        private Enrolment() { }

        public Enrolment(Member member, Lesson lesson, int? courseId, DateTime enrolmentDate)
        {
            MemberId = member.Id;
            LessonId = lesson.Id;
            CourseId = courseId;
            EnrolmentDate = enrolmentDate;
        }
    }
}