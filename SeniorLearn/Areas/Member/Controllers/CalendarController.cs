using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeniorLearn.Controllers;
using SeniorLearn.Data.Core;

namespace SeniorLearn.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Standard,Professional,Administrator,Honorary")]
    public class CalendarController : BaseController
    {
        public CalendarController(ApplicationDbContext context, ILogger<BaseController> logger) : base(context, logger) { }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}