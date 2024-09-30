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
        public IActionResult Create()
        {
            var lessonModel = new CreateLesson
            {
                StartDate = DateTime.UtcNow
            };
            PopulateLessonDropdowns(lessonModel);

            // Initialize the CreateCourse model and set it in ViewBag
            var createCourseModel = new CreateCourse
            {
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(30),
                Users = _lessonService.GetAllUsers().ToList() // Populate Users dropdown
            };
            ViewBag.CreateCourseModel = createCourseModel;

            return View(lessonModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(CreateLesson model)
        {
            if (!ModelState.IsValid)
            {
                PopulateLessonDropdowns(model);

                // Ensure ViewBag.CreateCourseModel is set if validation fails
                var createCourseModel = new CreateCourse
                {
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(30),
                    Users = _lessonService.GetAllUsers().ToList()
                };
                ViewBag.CreateCourseModel = createCourseModel;

                return View(model);
            }

            try
            {
                var user = HttpContext.User.Identity!.Name;
                await _lessonService.CreateLessonAsync(model, user!);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while creating the lesson. Please try again.");
                _logger.LogError(ex, "An unexpected error occurred while creating a lesson.");
            }

            PopulateLessonDropdowns(model);

            // Ensure ViewBag.CreateCourseModel is set if an exception occurs
            var fallbackCourseModel = new CreateCourse
            {
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(30),
                Users = _lessonService.GetAllUsers().ToList()
            };
            ViewBag.CreateCourseModel = fallbackCourseModel;

            return View(model);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var lessons = _lessonService.GetLessonsForCalendar();
            return View(lessons);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> CreateCourse(CreateCourse model)
        {
            if (!ModelState.IsValid)
            {
                PopulateCourseDropdowns(model);

                // Reinitialize the lesson model to prevent NullReferenceException
                var lessonModel = new CreateLesson
                {
                    StartDate = DateTime.UtcNow
                };
                PopulateLessonDropdowns(lessonModel);
                ViewBag.CreateCourseModel = model;

                return View("Create", lessonModel);
            }

            try
            {
                await _lessonService.CreateCourseAsync(model);
                ViewBag.CourseCreatedMessage = "Course created successfully!";

                // Reinitialize the lesson model to reflect changes
                var lessonModel = new CreateLesson
                {
                    StartDate = DateTime.UtcNow
                };
                PopulateLessonDropdowns(lessonModel);
                ViewBag.CreateCourseModel = new CreateCourse
                {
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(30),
                    Users = _lessonService.GetAllUsers().ToList()
                };

                return View("Create", lessonModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while creating the course. Please try again.");
                _logger.LogError(ex, "An unexpected error occurred while creating a course.");
            }

            PopulateCourseDropdowns(model);

            // Reinitialize the lesson model to prevent NullReferenceException
            var fallbackLessonModel = new CreateLesson
            {
                StartDate = DateTime.UtcNow
            };
            PopulateLessonDropdowns(fallbackLessonModel);
            ViewBag.CreateCourseModel = model;

            return View("Create", fallbackLessonModel);
        }

        private void PopulateLessonDropdowns(CreateLesson model)
        {
            model.DurationOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "30", Text = "30 minutes" },
                new SelectListItem { Value = "60", Text = "60 minutes" },
                new SelectListItem { Value = "90", Text = "90 minutes" }
            };
            model.DeliveryModes = new List<SelectListItem>
            {
                new SelectListItem { Value = "OnPremise", Text = "On Premise" },
                new SelectListItem { Value = "Virtual", Text = "Virtual" }
            };
            model.Courses = _lessonService.GetAllCourses().ToList();
        }

        private void PopulateCourseDropdowns(CreateCourse model)
        {
            model.Users = _lessonService.GetAllUsers().ToList(); // Populate Users dropdown
        }
    }
}
