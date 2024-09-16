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

        public OrganisationUserService(ApplicationDbContext context, UserManager<OrganisationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Member> RegisterMemberAsync(int organisationId, string firstName, string lastName, string email, string password = "1")
        {
            var member = new Member(organisationId, firstName, lastName, email, password)
            {
                OrganisationId = 1,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = email,
                Registered = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(member, password);

            if (result.Succeeded)
            {
                return member;
            }
            throw new ApplicationException(result.Errors.First().Description);
        }

        public async Task<IEnumerable<MemberDTO>> GetUsersAsync()
        {
            var users = await _context.Users.Select(u => new MemberDTO
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email!,
                RenewalDate = u.UserRoles.OrderByDescending(ur => ur.EndDate).FirstOrDefault()!.EndDate.ToShortDateString()
                ?? "No Role Assigned",
            }).ToListAsync();

            return users;
        }

        public async Task<IEnumerable<MemberDTO>> GetInactiveUsersAsync()
        {
            var inactiveUsers = await _context.Users.Where(u => u.UserRoles.Count == 0)
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

        public async Task<IEnumerable<MemberDTO>> GetActiveUsersAsync()
        {
            var activeUsers = await _context.Users.Where(u => u.UserRoles.Any(ur => ur.StartDate <= ur.EndDate))
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
    }
}