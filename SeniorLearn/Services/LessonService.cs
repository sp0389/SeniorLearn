using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SeniorLearn.Areas.Member.Models.Lesson;
using SeniorLearn.Areas.Member.Models.Course;
using SeniorLearn.Data;
using SeniorLearn.Data.Core;
using Microsoft.IdentityModel.Tokens;

namespace SeniorLearn.Services
{
    public class LessonService
    {
        private readonly ApplicationDbContext _context;
        private readonly OrganisationUserService _organisationUserService;

        public LessonService(ApplicationDbContext context, OrganisationUserService organisationUserService)
        {
            _context = context;
            _organisationUserService = organisationUserService;
        }

        public async Task CreateLessonAsync(CreateLesson model, string userId)
        {
            var member = await _organisationUserService.GetUserByUserNameAsync(userId);
            var timetable = new Timetable();
            var groupId = Guid.NewGuid();

            if (model.IsRecurring && model.SelectedDaysOfWeek.IsNullOrEmpty())
            {
                var endDate = model.EndDate ?? model.RecurringStartDate;
                var daily = new DailyRepeating(model.RecurringStartDate, endDate, model.Occurrences);

                var lessons = timetable.DailyLessons(daily, model.LessonName, model.Description, model.Duration, member, model.Location, model.DeliveryMode == "OnPremise" ? DeliveryType.OnCampus : DeliveryType.Virtual, model.IsCourse, model.SelectedCourseId, groupId);

                foreach (var lesson in lessons)
                {
                    await _context.Lessons.AddAsync(lesson);
                }
            }
            else if (model.IsRecurring)
            {
                var endDate = model.EndDate ?? model.RecurringStartDate;
                var weekly = new WeeklyRepeating(model.RecurringStartDate, endDate, model.Occurrences, model.SelectedDaysOfWeek);

                var lessons = timetable.WeeklyLessons(weekly, model.LessonName, model.Description, model.Duration, member, model.Location, model.DeliveryMode == "OnPremise" ? DeliveryType.OnCampus : DeliveryType.Virtual, model.IsCourse, model.SelectedCourseId, groupId);

                foreach (var lesson in lessons)
                {
                    await _context.Lessons.AddAsync(lesson);
                }
            }
            else
            {
                var lesson = timetable.CreateLesson(model.LessonName, model.Description, model.Duration, member, model.Location, model.SingleStartDate, model.DeliveryMode == "OnPremise" ? DeliveryType.OnCampus : DeliveryType.Virtual, model.IsCourse, model.SelectedCourseId, groupId);
                await _context.Lessons.AddAsync(lesson);
            }
            await _context.SaveChangesAsync();
        }

        public async Task CreateCourseAsync(Create model, string userId)
        {
            var member = await _organisationUserService.GetUserByUserNameAsync(userId);

            var course = new Course
            {
                Title = model.CourseName,
                Description = model.Description,
                UserId = member.Id,
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SelectListItem>> GetAllCourses()
        {
            var courses = await _context.Courses.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Title
            }).ToListAsync();

            return courses;
        }

        public async Task<IEnumerable<SelectListItem>> GetAllUsers()
        {
            var users = await _context.Users.Select(u => new SelectListItem
            {
                Value = u.Id,
                Text = u.FirstName
            }).ToListAsync();

            return users;
        }

        public async Task<IEnumerable<Lesson>> GetLessonsForCalendarAsync()
        {
            return await _context.Lessons.Include(l => l.Course).ToListAsync();
        }

        public async Task PopulateLessonDropdownsAsync(CreateLesson model)
        {
            model.DurationOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "30", Text = "30 minutes" },
                new SelectListItem { Value = "60", Text = "1 hour" },
                new SelectListItem { Value = "90", Text = "1 hour 30 minutes" },
                new SelectListItem { Value = "120", Text = "2 hours" },
                new SelectListItem { Value = "150", Text = "2 hours 30 minutes" },
                new SelectListItem { Value = "180", Text = "3 hours" }
            };

            model.DeliveryModes = new List<SelectListItem>
            {
                new SelectListItem { Value = "OnPremise", Text = "On Premise" },
                new SelectListItem { Value = "Virtual", Text = "Virtual" }
            };

            model.Courses = await GetAllCourses();
        }
    }
}