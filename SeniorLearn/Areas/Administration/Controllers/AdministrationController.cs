using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeniorLearn.Controllers;
using SeniorLearn.Data;
using SeniorLearn.Data.Core;

namespace SeniorLearn.Areas.Administration.Controllers
{
    [Area("Administration"), Authorize(Roles = "Administrator")]
    public abstract class AdministrationController : BaseController
    {
        public AdministrationController(ApplicationDbContext context, ILogger<AdministrationController> logger) : base(context, logger){}
        protected Member Member => _context.Users.OfType<Member>().First(m => m.UserName == User.Identity!.Name);
        protected int OrganisationId => Member.OrganisationId;
    }
}