using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SeniorLearn.Areas.Administration.Models.Lesson;
using SeniorLearn.Areas.Administration.Models.Course;
using SeniorLearn.Data;
using SeniorLearn.Data.Core;
using SeniorLearn.Data.Scheduling;
using SeniorLearn.Models.Enum;

namespace SeniorLearn.Services
{
    public class LessonService
    {
        private readonly ApplicationDbContext _context;

        public LessonService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateLessonAsync(CreateLesson model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var member = _context.Users.OfType<Member>().FirstOrDefault();
            if (member == null)
                throw new InvalidOperationException("Member not found.");

            Timetable timetable = new Timetable();

            // Create lessons based on frequency
            if (model.IsRecurring)
            {
                if (model.Frequency == "Daily")
                {
                    var dailySchedule = new DailyRepeating(model.StartDate, model.StartDate.AddDays(model.Occurrences - 1), model.Occurrences);
                    timetable.ScheduleDailyLessons(dailySchedule, model.LessonName, model.Description, model.Duration, member, model.Location, model.DeliveryMode == "OnPremise" ? DeliveryType.OnCampus : DeliveryType.Virtual);
                }
                else if (model.Frequency == "Weekly")
                {
                    var weeklySchedule = new WeeklyRepeating(model.StartDate, model.StartDate.AddDays(model.Occurrences * 7 - 1), model.Occurrences, new List<DayOfWeek> { model.StartDate.DayOfWeek });
                    timetable.ScheduleWeeklyLessons(weeklySchedule, model.LessonName, model.Description, model.Duration, member, model.Location, model.DeliveryMode == "OnPremise" ? DeliveryType.OnCampus : DeliveryType.Virtual);
                }
            }
            else
            {
                var lesson = new Lesson(model.LessonName, model.Description, model.Duration, member, model.Location, model.StartDate, model.StartDate.AddMinutes(model.Duration), model.DeliveryMode == "OnPremise" ? DeliveryType.OnCampus : DeliveryType.Virtual);
                if (model.IsCourse && model.SelectedCourseId.HasValue)
                {
                    lesson.CourseId = model.SelectedCourseId.Value;
                }
                _context.Lessons.Add(lesson);
            }

            _context.Lessons.AddRange(timetable.Lessons);
            await _context.SaveChangesAsync();
        }

        public async Task CreateCourseAsync(CreateCourse model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var course = new Course
            {
                Title = model.CourseName,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                UserId = model.SelectedUserId 
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<SelectListItem> GetAllCourses()
        {
            var courses = _context.Courses;
            return courses.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Title
            }).ToList();
        }

        public IEnumerable<SelectListItem> GetAllUsers()
        {
            // Fetch all users to populate the dropdown
            var users = _context.Users.ToList();
            return users.Select(u => new SelectListItem
            {
                Value = u.Id,
                Text = u.FirstName 
            });
        }

        public IEnumerable<Lesson> GetLessonsForCalendar()
        {
            return _context.Lessons.Include(l => l.Course).ToList();
        }
    }
}
