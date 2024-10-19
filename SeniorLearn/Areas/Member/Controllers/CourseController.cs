using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeniorLearn.Areas.Member.Models.Course;
using SeniorLearn.Controllers;
using SeniorLearn.Data.Core;
using SeniorLearn.Services;


namespace SeniorLearn.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Professional")]
    public class CourseController : BaseController
    {
        private readonly LessonService _lessonService;
        public CourseController(ApplicationDbContext context, ILogger<CourseController> logger, LessonService lessonService) : base(context, logger)
        {
            _lessonService = lessonService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var create = new Create()
            {
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddMonths(6)
            };

            return View(create);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Create m)
        {
            var member = HttpContext.User.Identity!.Name;

            if (ModelState.IsValid)
            {
                try
                {
                    await _lessonService.CreateCourseAsync(m, member!);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return RedirectToAction("Create", "Lesson");
        }
    }
}
