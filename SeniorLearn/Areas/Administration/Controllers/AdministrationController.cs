using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeniorLearn.Controllers;
using SeniorLearn.Data.Core;

namespace SeniorLearn.Areas.Administration.Controllers
{
    [Area("Administration"), Authorize(Roles = "Administrator")]
    public abstract class AdministrationController : BaseController
    {
        public AdministrationController(ApplicationDbContext context, ILogger<AdministrationController> logger) : base(context, logger){}
        protected int OrganisationId => 1;
    }
}