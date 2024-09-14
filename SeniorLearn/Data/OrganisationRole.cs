using Microsoft.AspNetCore.Identity;

namespace SeniorLearn.Data
{
    public enum RoleTypes
    {
        Administrator,
        Standard,
        Professional,
        Honorary
    }

    public class OrganisationUserRole : IdentityUserRole<string>
    {
        public OrganisationUser User { get; set; } = default!;
        public OrganisationRole Role { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public RoleTypes RoleType { get; set; }

        public OrganisationUserRole GrantProfessionalMember(DateTime startDate, int duration)
        {
            StartDate = new DateTime(startDate.Year, startDate.Month, 1).AddMonths(1);
            EndDate = duration == 3 ? StartDate.AddMonths(3) : StartDate.AddYears(1);
            RoleType = RoleTypes.Professional;
            return this;
        }

        public OrganisationUserRole GrantStandardMember(DateTime startDate)
        {
            StartDate = new DateTime(startDate.Year, startDate.Month, 1).AddMonths(1);
            EndDate = StartDate.AddYears(1);
            RoleType = RoleTypes.Standard;
            return this;
        }

        public OrganisationUserRole GrantHonoraryMember()
        {
            StartDate = DateTime.UtcNow;
            EndDate = DateTime.MaxValue;
            RoleType = RoleTypes.Honorary;
            return this;
        }
    }

    public class OrganisationRole : IdentityRole
    {
        public ICollection<OrganisationUserRole> UserRoles { get; set; } = new List<OrganisationUserRole>();

        public OrganisationRole() : base(){}
    }
}
