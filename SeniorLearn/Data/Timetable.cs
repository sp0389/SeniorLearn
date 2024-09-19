using SeniorLearn.Models.Enum;

namespace SeniorLearn.Data.Scheduling
{
    public class Timetable
    {
        public ICollection<Lesson> Lessons { get; private set; } = new List<Lesson>();

        public IEnumerable<Lesson> ScheduleDailyLessons(DailyRepeating schedule, string title, string description, int duration, Member member, string address, DeliveryType type)
        {
            var scheduledDates = schedule.GenerateSchedule();

            foreach (var startDate in scheduledDates)
            {
                var lesson = CreateLesson(title, description, duration, member, address, startDate, type);
                Lessons.Add(lesson);
            }
            return Lessons;
        }

        public IEnumerable<Lesson> ScheduleWeeklyLessons(WeeklyRepeating schedule, string title, string description, int duration, Member member, string address, DeliveryType type)
        {
            var scheduledDates = schedule.GenerateSchedule();

            foreach (var startDate in scheduledDates)
            {
                var lesson = CreateLesson(title, description, duration, member, address, startDate, type);
                Lessons.Add(lesson);
            }
            return Lessons;
        }

        private static Lesson CreateLesson(string title, string description, int duration, Member member, string address, DateTime startDate, DeliveryType type)
        {
            return new Lesson(title, description, duration, member, address, startDate, startDate.AddMinutes(duration), type);
        }
    }
}
