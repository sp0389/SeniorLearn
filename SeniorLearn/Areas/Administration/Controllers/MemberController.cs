using Microsoft.AspNetCore.Mvc;
using SeniorLearn.Areas.Administration.Models.Member;
using SeniorLearn.Data.Core;
using SeniorLearn.Services;

namespace SeniorLearn.Areas.Administration.Controllers
{
    public class MemberController : AdministrationController
    {
        private readonly OrganisationUserService _organisationUserService;

        public MemberController(ApplicationDbContext context, ILogger<MemberController> logger,
            OrganisationUserService organisationUserService)
            : base(context, logger)
        {
            _organisationUserService = organisationUserService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel u)
        {
            if (ModelState.IsValid)
            {
                var organisationId = 1;

                try
                {
                    await _organisationUserService.RegisterMemberAsync(organisationId, u.FirstName, u.LastName, u.Email);
                }
                catch (DomainRuleException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View();
        }
    }
}