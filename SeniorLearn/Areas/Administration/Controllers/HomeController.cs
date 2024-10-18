using Microsoft.AspNetCore.Mvc;
using SeniorLearn.Data.Core;

namespace SeniorLearn.Areas.Administration.Controllers
{
    public class HomeController : AdministrationController
    {
        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger) 
            : base(context, logger) {}

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}