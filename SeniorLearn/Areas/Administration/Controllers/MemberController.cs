using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SeniorLearn.Areas.Administration.Models.Member;
using SeniorLearn.Data;
using SeniorLearn.Data.Core;
using SeniorLearn.Services;

namespace SeniorLearn.Areas.Administration.Controllers
{
    public class MemberController : AdministrationController
    {
        private readonly OrganisationUserService _organisationUserService;
        private readonly OrganisationUserRoleService _organisationUserRoleService;
        private readonly UserManager<OrganisationUser> _userManager;

        public MemberController(ApplicationDbContext context, ILogger<MemberController> logger,
            OrganisationUserService organisationUserService, UserManager<OrganisationUser> userManager,
            OrganisationUserRoleService organisationUserRoleService)
            : base(context, logger)
        {
            _userManager = userManager;
            _organisationUserService = organisationUserService;
            _organisationUserRoleService = organisationUserRoleService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(bool? isActive)
        {
            switch (isActive)
            {
                case false:
                    var inactiveUsers = await _organisationUserService.GetInactiveUsersAsync();
                    return View(inactiveUsers);
                case true:
                    var activeUsers = await _organisationUserService.GetActiveUsersAsync();
                    return View(activeUsers);
                default:
                    var users = await _organisationUserService.GetUsersAsync();
                    return View(users);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register([Bind("FirstName, LastName, Email")] Register u)
        {
            if (ModelState.IsValid)
            {
                var organisationId = 1;

                try
                {
                    await _organisationUserService.RegisterMemberAsync(organisationId, u.FirstName, u.LastName, u.Email);
                    return RedirectToAction("Index");
                }
                catch (DomainRuleException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var assignedRoles = await _userManager.GetRolesAsync(user);

            // https://stackoverflow.com/questions/3489453/how-can-i-convert-an-enumeration-into-a-listselectlistitem
            var roleType = Enum.GetValues(typeof(RoleTypes)).Cast<RoleTypes>().Select(role => new SelectListItem
            {
                Text = role.ToString(),
                Value = ((int)role).ToString(),
                Disabled = assignedRoles.Contains(role.ToString()),
            }).ToList();

            var vm = new Edit
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                AssignedRoles = assignedRoles,
                RoleTypes = roleType
            };

            return View(vm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("RemoveRole", "Role")] string id, Edit u)
        {
            if (u.RemoveRole == true && u.Role != null)
            {
                try
                {
                    await _organisationUserRoleService.RemoveRoleFromUserAsync(id, u.Role);
                }
                catch (DomainRuleException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }

                return RedirectToAction("Edit");
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                try
                {
                    user.FirstName = u.FirstName;
                    user.LastName = u.LastName;
                    user.Email = u.Email;

                    await _organisationUserRoleService.AssignRoleAsync(user, DateTime.UtcNow, u.SelectedRole, u.Duration);
                    return RedirectToAction("Edit");
                }

                catch (DomainRuleException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(u);
        }
    }
}