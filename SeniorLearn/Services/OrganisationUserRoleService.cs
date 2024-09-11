using Microsoft.AspNetCore.Identity;
using SeniorLearn.Data.Core;
using SeniorLearn.Data;
using Microsoft.EntityFrameworkCore;

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

        public async Task AssignRoleAsync(string userId, DateTime startDate, RoleTypes? roleType, int duration)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ArgumentException("User does not exist in the database!");
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var role = await GetUserRoleAsync(roleType);

            if (roleType!.Value == RoleTypes.Standard && userRoles.Contains("Professional")
                || roleType!.Value == RoleTypes.Professional && userRoles.Contains("Standard"))
            {
                throw new ArgumentException("User cannot be a professional member and a standard member!");
            }

            var userRole = new OrganisationUserRole()
            {
                RoleId = role.Id,
                UserId = userId,
                User = user,
                Role = role,
            };

            switch (roleType)
            {
                case RoleTypes.Standard:
                    userRole.GrantStandardMember(startDate);
                    break;
                case RoleTypes.Professional:
                    userRole.GrantProfessionalMember(startDate, duration);
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

            return role ?? throw new InvalidOperationException("Role does not exist in the database!");
        }
    }
}