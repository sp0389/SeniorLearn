using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeniorLearn.Controllers;
using SeniorLearn.Data.Core;
using SeniorLearn.Services;

namespace SeniorLearn.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Standard,Professional,Administrator,Honorary")]
    public class EnrolmentController : BaseController
    {
        private readonly EnrolmentService _enrolmentService;
        public EnrolmentController(ApplicationDbContext context, ILogger<EnrolmentController> logger,  EnrolmentService enrolmentService)
            : base(context, logger)
        {
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
                var member = HttpContext.User.Identity!.Name;
                var lessonDetails = await _enrolmentService.GetLessonDetailsForEnrolmentAsync(id, member!);
                return View(lessonDetails);
            }
            catch (DomainRuleException ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Overview()
        {
            var member = HttpContext.User.Identity!.Name;

            var enrolments = await _enrolmentService.GetMemberLessonEnrolmentsAsync(member!);
            return View(enrolments);
        }

        //TODO: Add CSRF prevention
        [HttpPost]
        public async Task<IActionResult> Enrol(IList<int> Lessons)
        {
            if (ModelState.IsValid)
            {
                var member = HttpContext.User.Identity!.Name;
                try
                {
                    await _enrolmentService.EnrolMemberAsync(member!, Lessons);
                    TempData["Success"] = "You have been successfully enroled.";
                }
                catch (DomainRuleException ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }
            return RedirectToAction("Overview");
        }

        //TODO: Add CSRF prevention
        [HttpPost]
        public async Task<IActionResult> UnenrolCourseLessons(IList<int> Lessons, int id)
        {
            if (ModelState.IsValid)
            {
                var member = HttpContext.User.Identity!.Name;

                try
                {
                    await _enrolmentService.UnenrolMemberFromLessonAsync(member!, Lessons, id);
                    TempData["Success"] = "You have been unenroled.";
                }
                catch (DomainRuleException ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }
            return RedirectToAction("Overview");
        }
        
        //TODO: Add CSRF prevention
        [HttpPost]
        public async Task<IActionResult> UnenrolLessons(IList<int>Lessons, int id)
        {
            if (ModelState.IsValid)
            {
                var member = HttpContext.User.Identity!.Name;

                try
                {
                    await _enrolmentService.UnenrolMemberFromLessonAsync(member!, Lessons, id);
                    TempData["Success"] = "You have been unenroled from this lesson";
                }
                catch (DomainRuleException ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }
            return RedirectToAction("Overview");
        }
    }
}