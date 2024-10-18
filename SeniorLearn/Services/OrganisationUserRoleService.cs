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

        public async Task AssignRoleAsync(Member member, DateTime startDate, RoleTypes? roleType, int duration, DateTime? renewalDate)
        {
            if (roleType == null)
            {
                await _context.SaveChangesAsync();
            }

            else
            {
                var userRoles = await _userManager.GetRolesAsync(member);
                var role = await GetUserRoleAsync(roleType);

                if (roleType == RoleTypes.Professional && renewalDate == null && duration == 0)
                {
                    throw new DomainRuleException("You must select a renewal date.");
                }

                if (startDate >= renewalDate)
                {
                    throw new DomainRuleException("Renewal date must come after todays date.");
                }
                
                if (roleType == RoleTypes.Standard && userRoles.Contains("Professional") || roleType == RoleTypes.Professional && userRoles.Contains("Standard"))
                {   
                    throw new DomainRuleException("Member cannot hold a professional and a standard role at the same time.");
                }

                switch (roleType)
                {
                    case RoleTypes.Standard:
                        var standard = member.GrantStandardRole(member, role, startDate, renewalDate);
                        await _context.UserRoles.AddAsync(standard);
                    break;
                    case RoleTypes.Professional:
                        var professional = member.GrantProfessionalRole(member, role, startDate, duration, renewalDate);
                        await _context.UserRoles.AddAsync(professional);
                        break;
                    case RoleTypes.Honorary:
                        var honorary = member.GrantHonoraryRole(member, role, startDate);
                        await _context.UserRoles.AddAsync(honorary);
                        break;
                }

                await _context.SaveChangesAsync(); 
            }
        }
        
        public async Task<OrganisationRole> GetUserRoleAsync(RoleTypes? roleType)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleType.ToString()) ?? throw new ApplicationException("Role not found");
        }

        public async Task<IList<string>> GetUserRolesAsync(Member member)
        {
            return await _userManager.GetRolesAsync(member);
        }

        public async Task<bool> RemoveRoleFromUserAsync(string memberId, string role)
        {
            var member = await _userManager.FindByIdAsync(memberId);
            member!.Status = Status.Inactive;
            return (await _userManager.RemoveFromRoleAsync(member!, role)).Succeeded;
        }
    }
}