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
        public EnrolmentController(ApplicationDbContext context, ILogger<EnrolmentController> logger, LessonService lessonService) 
            : base(context, logger)
        {
            _lessonService = lessonService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var lessons = await _lessonService.GetLessonOverviewAsync();
            return View(lessons);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var user = HttpContext.User.Identity!.Name;
                var lessonDetails = await _lessonService.GetLessonDetailsAsync(id, user!);
                return View(lessonDetails);
            }
            catch (DomainRuleException ex)
            {
                //TODO: Hack & I hate it, but it does work. Should probably log it also.
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}