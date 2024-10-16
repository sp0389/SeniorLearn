using Microsoft.AspNetCore.Identity;
using SeniorLearn.Data.Core;
using SeniorLearn.Data;
using Microsoft.EntityFrameworkCore;
using SeniorLearn.Models;
using Mapster;

namespace SeniorLearn.Services
{
    public class OrganisationUserService
    {
        protected readonly ApplicationDbContext _context;
        protected readonly UserManager<OrganisationUser> _userManager;
        protected readonly OrganisationUserRoleService _organisationUserRoleService;

        public OrganisationUserService(ApplicationDbContext context, UserManager<OrganisationUser> userManager, OrganisationUserRoleService organisationUserRoleService)
        {
            _context = context;
            _userManager = userManager;
            _organisationUserRoleService = organisationUserRoleService;
        }

        public async Task<MemberDTO> RegisterMemberAsync(int organisationId, string firstName, string lastName, string email, string password = "1")
        {
            var organisation = await _context.Organisations.FindAsync(organisationId);
            var member = organisation!.RegisterMember(firstName, lastName, email);
            var result = await _userManager.CreateAsync(member, password);
            
            var role = await _organisationUserRoleService.GetUserRoleAsync(RoleTypes.Standard);
            var assignRole = member.GrantStandardRole(member, role, DateTime.UtcNow, default);
            
            await _context.UserRoles.AddAsync(assignRole);
            await _context.SaveChangesAsync();

            if (result.Succeeded)
            {
                return member.Adapt<MemberDTO>();
            }

            throw new ApplicationException(result.Errors.First().Description);
        }

        public async Task<OrganisationUser> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user!;
        }
        public async Task<Member> GetUserByUserNameAsync(string id)
        {
            var user = await _context.Users.OfType<Member>()
                .Where(u => u.UserName == id).FirstOrDefaultAsync();

            return user!;
        }

        public async Task<IEnumerable<MemberDTO>> GetUsersAsync(int skip = 0, int take = int.MaxValue)
        {
            var users = await _context.Users.OrderBy(u => u.LastName).Skip(skip).Take(take)
            .Select(u => new MemberDTO
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email!,
                RenewalDate = u.UserRoles.OrderByDescending(ur => ur.EndDate).FirstOrDefault()!.EndDate.ToShortDateString() ?? "No Role Assigned",
            }).ToListAsync();
            return users;
        }

        public async Task<IEnumerable<MemberDTO>> GetInactiveUsersAsync(int skip = 0, int take = int.MaxValue)
        {
            var inactiveUsers = await _context.Users.Where(u => u.UserRoles.Count == 0)
            .OrderBy(u => u.LastName).Skip(skip).Take(take)
                .Select(u => new MemberDTO
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email!,
                    RenewalDate = "No Role Assigned",
                }).ToListAsync();
                
            return inactiveUsers;
        }

        public async Task<IEnumerable<MemberDTO>> GetActiveUsersAsync(int skip = 0, int take = int.MaxValue)
        {
            var activeUsers = await _context.Users.Include(u => u.UserRoles)
            .Where(u => u.UserRoles.Count != 0)
            .OrderBy(u => u.LastName).Skip(skip).Take(take)
                .Select(u => new MemberDTO
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email!,
                    RenewalDate = u.UserRoles.OrderByDescending(ur => ur.EndDate).FirstOrDefault()!.EndDate.ToShortDateString(),
                }).ToListAsync();
                
            return activeUsers;
        }

        public async Task<int> GetUsersCountAsync()
        {
            var userCount = await _context.Users.CountAsync();
            
            return userCount;
        }

        public async Task<int> GetInactiveUserCountAsync()
        {
            var inactiveUserCount = await _context.Users.Where(u => u.UserRoles.Count == 0)
            .CountAsync();

            return inactiveUserCount;
        }

        public async Task<int> GetActiveUserCountAsync()
        {
            var activeUserCount = await _context.Users.Where(u => u.UserRoles.Count != 0)
            .CountAsync();
            
            return activeUserCount;
        }
    }
}