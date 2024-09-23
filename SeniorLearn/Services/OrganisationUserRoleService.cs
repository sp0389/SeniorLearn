using Microsoft.AspNetCore.Identity;
using SeniorLearn.Data.Core;
using SeniorLearn.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            if (roleType == null)
            {
                await _context.SaveChangesAsync();
            }

            else
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var role = await GetUserRoleAsync(roleType);

                var assignUserRole = new OrganisationUserRole(user, role);
                assignUserRole.RoleValidationCheck(startDate, roleType, duration, renewalDate, userRoles);

                switch (roleType)
                {
                    case RoleTypes.Standard:
                        assignUserRole.GrantStandardRole(startDate, renewalDate);
                        break;
                    case RoleTypes.Professional:
                        assignUserRole.GrantProfessionalRole(startDate, duration, renewalDate);
                        break;
                    case RoleTypes.Honorary:
                        assignUserRole.GrantHonoraryRole();
                        break;
                }
                await _context.UserRoles.AddAsync(assignUserRole);
                await _context.SaveChangesAsync(); 
            }
        }
        
        public async Task<OrganisationRole> GetUserRoleAsync(RoleTypes? roleType)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleType.ToString());

            return role!;
        }

        public async Task<IList<string>> GetUserRolesAsync(OrganisationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<bool> RemoveRoleFromUserAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return (await _userManager.RemoveFromRoleAsync(user!, role)).Succeeded;
        }
    }
}