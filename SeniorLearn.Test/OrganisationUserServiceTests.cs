using Microsoft.AspNetCore.Identity;
using SeniorLearn.Data;
using SeniorLearn.Data.Core;
using SeniorLearn.Services;

namespace SeniorLearn.Test
{
    public class OrganisationUserServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly OrganisationUserService _organisationUserService;
        private readonly UserManager<OrganisationUser> _userManager;

        public OrganisationUserServiceTests(OrganisationUserService organisationUserService, ApplicationDbContext context,
            UserManager<OrganisationUser> userManager)
        {
            _context = context;
            _organisationUserService = organisationUserService;
            _userManager = userManager;
        }

        [Fact]
        public async Task Should_RegisterANewMember()
        {
            var organisationId = 1;
            var password = "1";

            var o = await _context.Organisations.FindAsync(organisationId);
            var m = o!.RegisterMember("test", "testing", "test@testing.com.au");

            var user = await _userManager.CreateAsync(m, password);
            Assert.IsType<Member>(m);
            Assert.NotNull(user);
        }
    }
}
