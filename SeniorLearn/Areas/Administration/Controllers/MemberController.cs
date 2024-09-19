using Microsoft.AspNetCore.Mvc;
using SeniorLearn.Areas.Administration.Models.Member;
using SeniorLearn.Data.Core;
using SeniorLearn.Services;

namespace SeniorLearn.Areas.Administration.Controllers
{
    public class MemberController : AdministrationController
    {
        private readonly OrganisationUserService _organisationUserService;
        private readonly OrganisationUserRoleService _organisationUserRoleService;

        public MemberController(ApplicationDbContext context, ILogger<MemberController> logger,
            OrganisationUserService organisationUserService, OrganisationUserRoleService organisationUserRoleService)
            : base(context, logger)
        {
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
        public async Task<IActionResult> Register(Register u)
        {
            if (ModelState.IsValid)
            {
                var organisationId = 1;

                try
                {
                    await _organisationUserService.RegisterMemberAsync(organisationId, u.FirstName!, u.LastName!, u.Email!);
                    return RedirectToAction("Index");
                }

                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _organisationUserService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var assignedRoles = await _organisationUserRoleService.GetUserRolesAsync(user);
            var roleType = _organisationUserRoleService.MapRolesToSelectList(assignedRoles);

            var vm = new Edit
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                AssignedRoles = assignedRoles,
                RoleTypes = roleType,
            };

            return View(vm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit(string id, Edit u)
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
                
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }

                return RedirectToAction("Edit");
            }

            if (ModelState.IsValid)
            {
                var user = await _organisationUserService.GetUserByIdAsync(id);
                var assignedRoles = await _organisationUserRoleService.GetUserRolesAsync(user);
                var roleType = _organisationUserRoleService.MapRolesToSelectList(assignedRoles);

                if (user == null)
                {
                    return NotFound();
                }

                try
                {
                    user.FirstName = u.FirstName!;
                    user.LastName = u.LastName!;
                    user.Email = u.Email;
                    u.AssignedRoles = assignedRoles;
                    u.RoleTypes = roleType;

                    await _organisationUserRoleService.AssignRoleAsync(user, DateTime.UtcNow.Date, u.SelectedRole, u.Duration, u.RenewalDate);
                    return RedirectToAction("Edit");
                }

                catch (DomainRuleException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }

                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(u);
        }
    }
}