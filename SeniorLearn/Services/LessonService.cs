using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SeniorLearn.Areas.Member.Models.Lesson;
using SeniorLearn.Areas.Member.Models.Course;
using SeniorLearn.Data;
using SeniorLearn.Data.Core;

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
            var groupId = Guid.NewGuid();

            // Check if it's a recurring lesson
            if (model.IsRecurring)
            {
                var startDate = model.StartDate;
                var endDate = model.EndDate ?? startDate; // Default to start date if no end date provided
                int occurrences = model.Occurrences;

                // Handle recurrence by occurrence number if no end date is provided
                if (occurrences > 0 && !model.EndDate.HasValue)
                {
                    for (int i = 0; i < occurrences; i++)
                    {
                        var lessonStartDate = startDate.AddDays(i);

                        // Create and log the lesson
                        var lesson = new Lesson(
                            model.LessonName,
                            model.Description,
                            model.Duration,
                            member!,
                            model.Location,
                            lessonStartDate,
                            lessonStartDate.AddMinutes(model.Duration),
                            model.DeliveryMode == "OnPremise" ? DeliveryType.OnCampus : DeliveryType.Virtual,
                            model.IsCourse,
                            model.SelectedCourseId,
                            groupId
                        );
                        _context.Lessons.Add(lesson);
                    }
                }
                // Handle recurrence by end date
                else if (model.EndDate.HasValue)
                {
                    occurrences = (int)(endDate.Date - startDate.Date).TotalDays + 1; // Calculate days between start and end date

                    for (int i = 0; i < occurrences; i++)
                    {
                        var lessonStartDate = startDate.AddDays(i);

                        // Stop if we go past the end date
                        if (lessonStartDate > endDate)
                        {
                            break;
                        }

                        // Create and log the lesson
                        var lesson = new Lesson(
                            model.LessonName,
                            model.Description,
                            model.Duration,
                            member!,
                            model.Location,
                            lessonStartDate,
                            lessonStartDate.AddMinutes(model.Duration),
                            model.DeliveryMode == "OnPremise" ? DeliveryType.OnCampus : DeliveryType.Virtual,
                            model.IsCourse,
                            model.SelectedCourseId,
                            groupId
                        );
                        _context.Lessons.Add(lesson);
                    }
                }
            }
            else
            {
                // Create a single lesson for non-recurring cases
                var lesson = new Lesson(
                    model.LessonName,
                    model.Description,
                    model.Duration,
                    member!,
                    model.Location,
                    model.StartDate,
                    model.StartDate.AddMinutes(model.Duration),
                    model.DeliveryMode == "OnPremise" ? DeliveryType.OnCampus : DeliveryType.Virtual,
                    model.IsCourse,
                    model.SelectedCourseId,
                    groupId
                );
                _context.Lessons.Add(lesson);
            }

            // Save all changes to the database
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
                UserId = model.SelectedUserId!
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
            model.DaysOfWeekOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = DayOfWeek.Monday.ToString(), Text = "Monday" },
                new SelectListItem { Value = DayOfWeek.Tuesday.ToString(), Text = "Tuesday" },
                new SelectListItem { Value = DayOfWeek.Wednesday.ToString(), Text = "Wednesday" },
                new SelectListItem { Value = DayOfWeek.Thursday.ToString(), Text = "Thursday" },
                new SelectListItem { Value = DayOfWeek.Friday.ToString(), Text = "Friday" },
                new SelectListItem { Value = DayOfWeek.Saturday.ToString(), Text = "Saturday" },
                new SelectListItem { Value = DayOfWeek.Sunday.ToString(), Text = "Sunday" }
            };
        }
    }
}
