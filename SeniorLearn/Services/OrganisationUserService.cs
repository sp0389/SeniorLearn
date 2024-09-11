using Microsoft.AspNetCore.Identity;
using SeniorLearn.Data.Core;
using SeniorLearn.Data;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<OrganisationUser>> GetUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<IEnumerable<OrganisationUser>> GetInactiveUsersAsync()
        {
            var users = await _context.Users.Where(u => u.UserRoles.Count == 0).ToListAsync();
            return users;
        }

        public async Task<IEnumerable<OrganisationUser>> GetActiveUsersAsync()
        {
            var users = await _context.Users.Where(u => u.UserRoles.Any(ur => ur.StartDate <= ur.EndDate)).ToListAsync();
            return users;
        }
    }
}