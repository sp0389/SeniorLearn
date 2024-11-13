using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SeniorLearn.Areas.Member.Models.Lesson;
using SeniorLearn.Areas.Member.Models.Course;
using SeniorLearn.Data;
using SeniorLearn.Data.Core;
using Microsoft.IdentityModel.Tokens;
using Mapster;
using SeniorLearn.Models;

namespace SeniorLearn.Services
{
    public class LessonService
    {
        private readonly IWebHostEnvironment _webHost;
        private readonly ApplicationDbContext _context;
        private readonly OrganisationUserService _organisationUserService;

        public LessonService(ApplicationDbContext context, OrganisationUserService organisationUserService,
            IWebHostEnvironment webHost)
        {
            _context = context;
            _organisationUserService = organisationUserService;
            _webHost = webHost;
        }

        public async Task CreateLessonAsync(CreateLesson model, string userId)
        {
            var member = await _organisationUserService.GetUserByUserNameAsync(userId);
            var timetable = new Timetable();
            var groupId = Guid.NewGuid();
            var relativeImagePath = "";
            
            // save an image
            if (model.ImageUrl != null)
            {
                var imageFolder = Path.Combine(_webHost.WebRootPath, "images");
                var imageName = Path.GetFileName(model.ImageUrl.FileName);
                var imageSavePath = Path.Combine(imageFolder, imageName);
                relativeImagePath = $"~/images/{imageName}";

                using var stream = new FileStream(imageSavePath, FileMode.Create);
                await model.ImageUrl.CopyToAsync(stream);
            }
            
            if (model.IsRecurring && model.SelectedDaysOfWeek.IsNullOrEmpty())
            {
                if (model.RecurringStartDate > model.EndDate)
                {
                    throw new DomainRuleException("Start date must be before end date!");
                }
                
                var endDate = model.EndDate ?? model.RecurringStartDate;
                var daily = new DailyRepeating(model.RecurringStartDate, endDate, model.Occurrences);

                var lessons = timetable.DailyLessons(daily, model.LessonName, model.Description, model.Duration, member, model.Location, model.DeliveryMode == "OnPremise" ? DeliveryType.OnCampus : DeliveryType.Virtual, model.IsCourse, model.SelectedCourseId, groupId);

                foreach (var lesson in lessons)
                {
                    await _context.Lessons.AddAsync(lesson);
                    lesson.ImageUrl = relativeImagePath;
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
                    lesson.ImageUrl = relativeImagePath;
                }
            }
            else
            {
                var lesson = timetable.CreateLesson(model.LessonName, model.Description, model.Duration, member, model.Location, model.SingleStartDate, model.DeliveryMode == "OnPremise" ? DeliveryType.OnCampus : DeliveryType.Virtual, model.IsCourse, model.SelectedCourseId, groupId);
                await _context.Lessons.AddAsync(lesson);
                lesson.ImageUrl = relativeImagePath;
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

        public async Task<IEnumerable<LessonDTO>> GetLessonsForCalendarAsync(string memberId)
        {
            var member = await _organisationUserService.GetUserByUserNameAsync(memberId);
            return await _context.Lessons.Where(l => l.MemberId == member.Id)
            .ProjectToType<LessonDTO>()
            .ToListAsync();
        }

        public async Task<IEnumerable<LessonDTO>> GetLessonsForIndexAsync(string memberId)
        {
            var member = await _organisationUserService.GetUserByUserNameAsync(memberId);
            var lessons = await _context.Lessons.Where(l => l.MemberId == member.Id)
                .ProjectToType<LessonDTO>()
                .ToListAsync();
            return lessons.GroupBy(l => l.GroupId).Select(l => l.First()).ToList();
        }

        public async Task<IEnumerable<LessonDTO>> GetLessonDetailsAsync(Guid id, int skip = 0, int take = int.MaxValue)
        {
            return await _context.Lessons
                .Where(l => l.GroupId == id).Skip(skip).Take(take)
                .ProjectToType<LessonDTO>()
                .ToListAsync();
        }
        
        public async Task<int> GetLessonCountAsync(Guid id)
        {
            var lessons = await _context.Lessons.Where(l => l.GroupId == id)
                .ToListAsync();
            return lessons.Count;
        }

        public async Task UpdateLessonStateAsync(IList<int> Lessons, string lessonState)
        {
            var lessons = await _context.Lessons.Where(l => Lessons.Contains(l.Id))
                .ToListAsync();

            foreach (var lesson in lessons)
            {
                lesson.SetInitialLessonState(lesson);

                switch (lessonState)
                {
                    case "Scheduled":
                        lesson.Schedule();
                        break;
                    case "Cancelled":
                        lesson.Cancel();
                        break;
                    case "Complete":
                        lesson.Complete();
                        break;
                    case "Closed":
                        lesson.Close();
                        break;
                }
            }
            await _context.SaveChangesAsync();
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