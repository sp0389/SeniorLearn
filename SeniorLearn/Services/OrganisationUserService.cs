using Microsoft.AspNetCore.Identity;
using SeniorLearn.Data.Core;
using SeniorLearn.Data;

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
    }
}