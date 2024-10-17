using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SeniorLearn.Areas.Member.Models.Lesson;
using SeniorLearn.Areas.Member.Models.Course;
using SeniorLearn.Data.Core;
using SeniorLearn.Services;
using SeniorLearn.Controllers;

namespace SeniorLearn.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Professional")]
    public class LessonController : BaseController
    {
        private readonly LessonService _lessonService;

        public LessonController(ApplicationDbContext context, ILogger<LessonController> logger, LessonService lessonService)
            : base(context, logger)
        {
            _lessonService = lessonService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var lessonModel = new CreateLesson
            {
                StartDate = DateTime.UtcNow
            };

            await _lessonService.PopulateLessonDropdownsAsync(lessonModel);

            // Initialize the CreateCourse model and set it in ViewBag
            var createCourseModel = new CreateCourse
            {
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(30),
            };
            ViewBag.CreateCourseModel = createCourseModel;

            return View(lessonModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(CreateLesson model, string StartDateHidden, string EndDateHidden)
        {
            // Remove validation for hidden fields when occurrences are provided
            if (model.IsRecurring && model.Occurrences > 0)
            {
                ModelState.Remove("StartDateHidden");
                ModelState.Remove("EndDateHidden");
            }
            else if (!model.IsRecurring)
            {
                ModelState.Remove("StartDateHidden");
                ModelState.Remove("EndDateHidden");
            }

            if (!ModelState.IsValid)
            {
                await _lessonService.PopulateLessonDropdownsAsync(model);
                return View(model);
            }

            // Handle recurring lesson dates
            if (model.IsRecurring)
            {
                if (!string.IsNullOrEmpty(StartDateHidden))
                {
                    model.StartDate = DateTime.Parse(StartDateHidden);
                }

                if (!string.IsNullOrEmpty(EndDateHidden))
                {
                    model.EndDate = DateTime.Parse(EndDateHidden);
                }
                else if (model.Occurrences > 0)
                {
                    model.EndDate = null;
                }

                try
                {
                    var user = HttpContext.User.Identity!.Name;
                    await _lessonService.CreateLessonAsync(model, user!);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            else
            {
                model.EndDate = null;

                try
                {
                    var user = HttpContext.User.Identity!.Name;
                    await _lessonService.CreateLessonAsync(model, user!);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            await _lessonService.PopulateLessonDropdownsAsync(model);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var lessons = await _lessonService.GetLessonsForCalendarAsync();
            return View(lessons);
        }
    }
}
