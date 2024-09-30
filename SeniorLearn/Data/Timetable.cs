namespace SeniorLearn.Data.Schedule
{
    public class Timetable
    {
        public ICollection<Lesson> Lessons { get; private set; } = new List<Lesson>();

        public IEnumerable<Lesson> ScheduleDailyLessons(DailyRepeating schedule, string title, string description, int duration, Member member, string address, DeliveryType type, bool isInCourse, int? courseId, Guid groupId)
        {
            var scheduledDates = schedule.GenerateSchedule();

            foreach (var startDate in scheduledDates)
            {
                var lesson = CreateLesson(title, description, duration, member, address, startDate, type, isInCourse, courseId, groupId);
                Lessons.Add(lesson);
            }
            return Lessons;
        }

        public IEnumerable<Lesson> ScheduleWeeklyLessons(WeeklyRepeating schedule, string title, string description, int duration, Member member, string address, DeliveryType type, bool isInCourse, int? courseId, Guid groupId)
        {
            var scheduledDates = schedule.GenerateSchedule();

            foreach (var startDate in scheduledDates)
            {
                var lesson = CreateLesson(title, description, duration, member, address, startDate, type, isInCourse, courseId, groupId);
                Lessons.Add(lesson);
            }
            return Lessons;
        }

        private static Lesson CreateLesson(string title, string description, int duration, Member member, string address, DateTime startDate, DeliveryType type, bool isInCourse, int? courseId, Guid groupId)
        {
            return new Lesson(title, description, duration, member, address, startDate, startDate.AddMinutes(duration), type, isInCourse, courseId, groupId);
        }
    }
}
