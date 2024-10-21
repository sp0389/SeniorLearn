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
            return View();
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