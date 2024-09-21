using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeniorLearn.Data;
using SeniorLearn.Data.Core;

namespace SeniorLearn.Areas.Administration.Controllers
{
    [Area("Administration"), Authorize(Roles = "Administrator")]
    public abstract class AdministrationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        public AdministrationController(ApplicationDbContext context, ILogger<AdministrationController> logger) 
        {
            _context = context;
            _logger = logger;
        }
        protected Member Member => _context.Users.OfType<Member>().First(m => m.UserName == User.Identity!.Name);
        
        protected int OrganisationId => Member.OrganisationId;
    }
}