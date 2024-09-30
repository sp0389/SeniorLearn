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
    }
}