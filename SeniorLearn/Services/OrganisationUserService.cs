using Microsoft.AspNetCore.Identity;
using SeniorLearn.Data.Core;
using SeniorLearn.Data;
using Microsoft.EntityFrameworkCore;
using SeniorLearn.Models;

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

        public async Task<Member> RegisterMemberAsync(int organisationId, string firstName, string lastName, string email, string password = "1")
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
                return member;
            }

            throw new ApplicationException(result.Errors.First().Description);
        }

        public async Task<Member> GetUserByIdAsync(string id)
        {
            return await _context.Users.OfType<Member>()
                .Where(u => u.Id == id).FirstOrDefaultAsync() ?? throw new ApplicationException("User not found");
        }
        public async Task<Member> GetUserByUserNameAsync(string id)
        {
            return await _context.Users.OfType<Member>()
                .Where(u => u.UserName == id).FirstOrDefaultAsync() ?? throw new ApplicationException("User not found");
        }

        public async Task<IEnumerable<MemberDTO>> GetUsersAsync(int skip = 0, int take = int.MaxValue)
        {
            return await _context.Users.OfType<Member>().OrderBy(u => u.LastName).Skip(skip).Take(take)
            .Select(u => new MemberDTO
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email!,
                RenewalDate = u.UserRoles.OrderByDescending(ur => ur.EndDate).FirstOrDefault()!.EndDate.ToShortDateString() ?? "No Role Assigned",
            }).ToListAsync();
        }

        public async Task<IEnumerable<MemberDTO>> GetInactiveUsersAsync(int skip = 0, int take = int.MaxValue)
        {
            return await _context.Users.OfType<Member>().Where(u => u.Status == Status.Inactive)
            .OrderBy(u => u.LastName).Skip(skip).Take(take)
                .Select(u => new MemberDTO
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email!,
                    RenewalDate = "No Role Assigned",
                }).ToListAsync();
        }

        public async Task<IEnumerable<MemberDTO>> GetActiveUsersAsync(int skip = 0, int take = int.MaxValue)
        {
            return await _context.Users.OfType<Member>().Where(u => u.Status == Status.Active)
            .OrderBy(u => u.LastName).Skip(skip).Take(take)
                .Select(u => new MemberDTO
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email!,
                    RenewalDate = u.UserRoles.OrderByDescending(ur => ur.EndDate).FirstOrDefault()!.EndDate.ToShortDateString(),
                }).ToListAsync();
        }

        public async Task<int> GetUsersCountAsync()
        {
            return await _context.Users.OfType<Member>().CountAsync();
            
        }

        public async Task<int> GetInactiveUserCountAsync()
        {
            return await _context.Users.OfType<Member>().Where(u => u.Status == Status.Inactive)
                .CountAsync();
        }

        public async Task<int> GetActiveUserCountAsync()
        {
            return await _context.Users.OfType<Member>().Where(u => u.Status == Status.Active)
                .CountAsync();
        }
    }
}