using SeniorLearn.Data;
using SeniorLearn.Services;

namespace SeniorLearn.Test
{
    public class OrganisationUserRoleTests
    {
        private readonly OrganisationUserRoleService _organisationUserRoleService;
        private readonly OrganisationUserService _organisationUserService;

        public OrganisationUserRoleTests(OrganisationUserRoleService organisationUserRoleService, OrganisationUserService organisationUserService)
        {
            _organisationUserRoleService = organisationUserRoleService;
            _organisationUserService = organisationUserService;
        }

        [Fact]
        public async Task Should_AssignMemberWithStandardRole()
        {
            var role = await _organisationUserRoleService.GetUserRoleAsync(RoleTypes.Standard);
            var member = await _organisationUserService.GetUserByUserNameAsync("a.admin@seniorlearn.com.au");

            var expectedStartDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1).AddMonths(1);
            var expectedEndDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1).AddMonths(1).AddYears(1);

            var standardMember = member.GrantStandardRole(member, role, DateTime.UtcNow.Date, default);
            
            Assert.NotNull(standardMember);
            Assert.Equal(RoleTypes.Standard, standardMember.RoleType);
            Assert.Equal(expectedStartDate, standardMember.StartDate);
            Assert.Equal(expectedEndDate, standardMember.EndDate);
        }

        [Fact]
        public async Task Should_AssignMemberWithProfessionalRole()
        {
            var role = await _organisationUserRoleService.GetUserRoleAsync(RoleTypes.Professional);
            var member = await _organisationUserService.GetUserByUserNameAsync("a.admin@seniorlearn.com.au");

            var expectedStartDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1).AddMonths(1);
            var expectedEndDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1).AddMonths(1).AddYears(1);

            var professionalMember = member.GrantProfessionalRole(member, role, DateTime.UtcNow.Date, 0, default);
            
            Assert.NotNull(professionalMember);
            Assert.Equal(RoleTypes.Professional, professionalMember.RoleType);
            Assert.Equal(expectedStartDate, professionalMember.StartDate);
            Assert.Equal(expectedEndDate, professionalMember.EndDate);
        }

        [Fact]
        public async Task Should_AssignMemberWithHonoraryRole()
        {
            var role = await _organisationUserRoleService.GetUserRoleAsync(RoleTypes.Honorary);
            var member = await _organisationUserService.GetUserByUserNameAsync("a.admin@seniorlearn.com.au");

            var expectedStartDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1).AddMonths(1);
            var expectedEndDate = DateTime.MaxValue;

            var honoraryMember = member.GrantHonoraryRole(member, role, DateTime.UtcNow);
            
            Assert.NotNull(honoraryMember);
            Assert.Equal(RoleTypes.Honorary, honoraryMember.RoleType);
            Assert.Equal(expectedStartDate, honoraryMember.StartDate);
            Assert.Equal(expectedEndDate, honoraryMember.EndDate);
        }

        [Fact]
        public async Task Should_RemoveRoleFromMember()
        {
            var member = await _organisationUserService.GetUserByUserNameAsync("a.admin@seniorlearn.com.au");
            var startDate = DateTime.UtcNow;
            var roleType = RoleTypes.Standard;
            var duration = 12;

            await _organisationUserRoleService.AssignRoleAsync(member, startDate, roleType, duration, default);
            var removeRole = await _organisationUserRoleService.RemoveRoleFromUserAsync(member.Id, RoleTypes.Standard.ToString());
            
            Assert.True(removeRole);
        }
    }
}