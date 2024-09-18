using Microsoft.AspNetCore.Identity;
using SeniorLearn.Data.Core;
using SeniorLearn.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using SeniorLearn.Models;
using Mapster;

namespace SeniorLearn.Services
{
    public class OrganisationUserRoleService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<OrganisationUser> _userManager;

        public OrganisationUserRoleService(ApplicationDbContext context, UserManager<OrganisationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IEnumerable<SelectListItem> MapRolesToSelectList(IList<string> assignedRoles)
        {
            return Enum.GetValues(typeof(RoleTypes)).Cast<RoleTypes>().Select(role => new SelectListItem
            {
                Text = role.ToString(),
                Value = ((int)role).ToString(),
                Disabled = assignedRoles.Contains(role.ToString()),
            }).ToList();
        }

        public async Task AssignRoleAsync(OrganisationUser user, DateTime startDate, RoleTypes? roleType, int duration, DateTime? renewalDate)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var role = await GetUserRoleAsync(roleType);

            if (roleType == RoleTypes.Professional && renewalDate == null && duration == 0)
            {
                throw new ArgumentException("You must select a renewal date!");
            }    

            if (startDate >= renewalDate)
            {
                throw new ArgumentException("Renewal date must come after todays date!");
            }

            if (roleType == RoleTypes.Standard && userRoles.Contains("Professional")
                || roleType == RoleTypes.Professional && userRoles.Contains("Standard"))
            {
                throw new ArgumentException("Member cannot hold a professional and a standard role at the same time!");
            }

            var userRole = new OrganisationUserRole()
            {
                RoleId = role.Id,
                UserId = user.Id,
            };

            switch (roleType)
            {
                case RoleTypes.Standard:
                    userRole.GrantStandardMember(startDate, renewalDate);
                    break;
                case RoleTypes.Professional:
                    userRole.GrantProfessionalMember(startDate, duration, renewalDate);
                    break;
                case RoleTypes.Honorary:
                    userRole.GrantHonoraryMember();
                    break;
            }
            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();
        }
        public async Task<OrganisationRole> GetUserRoleAsync(RoleTypes? roleType)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleType.ToString());

            return role!;
        }

        public async Task<IList<string>> GetUserRolesAsync(OrganisationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            return userRoles;
        }

        public async Task<bool> RemoveRoleFromUserAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);

            return (await _userManager.RemoveFromRoleAsync(user!, role)).Succeeded;
        }
    }
}