using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeniorLearn.Data.Core;

namespace SeniorLearn.Areas.Administration.Controllers
{
    [Area("Administration"), Authorize(Roles = "Administrator")]
    public abstract class AdministrationController : Controller
    {
        public AdministrationController(ApplicationDbContext context, ILogger<AdministrationController> logger) 
        {
        }
    }
}