using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeniorLearn.Controllers;
using SeniorLearn.Data.Core;
using SeniorLearn.Services;

namespace SeniorLearn.Areas.Member1.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Standard,Professional,Administrator,Honorary")]
    public class EnrolmentController : BaseController
    {
        private readonly LessonService _lessonService;
        private readonly EnrolmentService _enrolmentService;
        public EnrolmentController(ApplicationDbContext context, ILogger<EnrolmentController> logger, LessonService lessonService, EnrolmentService enrolmentService) 
            : base(context, logger)
        {
            _lessonService = lessonService;
            _enrolmentService = enrolmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var lessons = await _enrolmentService.GetLessonOverviewForEnrolmentAsync();
            return View(lessons);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var user = HttpContext.User.Identity!.Name;
                var lessonDetails = await _enrolmentService.GetLessonDetailsForEnrolmentAsync(id, user!);
                return View(lessonDetails);
            }
            catch (DomainRuleException ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Enrol(IList<int> Lessons)
        {
            if (ModelState.IsValid)
            {
                var user = HttpContext.User.Identity!.Name;
                try
                {
                    await _enrolmentService.EnrolMemberAsync(user!, Lessons);
                    TempData["Success"] = "You have been successfully enroled.";
                }
                catch (DomainRuleException ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }
            return RedirectToAction("Index");
        }
    }
}