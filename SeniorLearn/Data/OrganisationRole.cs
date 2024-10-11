using Microsoft.AspNetCore.Identity;
using SeniorLearn.Data.Core;

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

        public OrganisationUserRole() { }

        public OrganisationUserRole(OrganisationUser user, OrganisationRole role)
        {
            User = user;
            Role = role;
        }

        public void GrantProfessionalRole(DateTime startDate, int duration, DateTime? renewalDate)
        {
            StartDate = new DateTime(startDate.Year, startDate.Month, 1).AddMonths(1);
            if (renewalDate.HasValue)
            {
                EndDate = renewalDate.Value;
            }
            else
            {
                EndDate = duration == 3 ? StartDate.AddMonths(3) : StartDate.AddYears(1);
            }
            RoleType = RoleTypes.Professional;
        }

        public void GrantStandardRole(DateTime startDate, DateTime? renewalDate)
        {
            StartDate = new DateTime(startDate.Year, startDate.Month, 1).AddMonths(1);
            EndDate = renewalDate ?? StartDate.AddYears(1);
            RoleType = RoleTypes.Standard;
        }

        public void GrantHonoraryRole()
        {
            StartDate = DateTime.UtcNow;
            EndDate = DateTime.MaxValue;
            RoleType = RoleTypes.Honorary;
        }

        public void RoleValidationCheck(DateTime startDate, RoleTypes? roleType, int duration, DateTime? renewalDate, IEnumerable<string> userRoles)
        {
            if (roleType == RoleTypes.Professional && renewalDate == null && duration == 0)
            {
                throw new DomainRuleException("You must select a renewal date.");
            }

            if (startDate >= renewalDate)
            {
                throw new DomainRuleException("Renewal date must come after todays date.");
            }

            if (roleType == RoleTypes.Standard && userRoles.Contains("Professional")
                || roleType == RoleTypes.Professional && userRoles.Contains("Standard"))
            {
                throw new DomainRuleException("Member cannot hold a professional and a standard role at the same time.");
            }
        }
    }
    public class OrganisationRole : IdentityRole
    {
        public ICollection<OrganisationUserRole> UserRoles { get; set; } = new List<OrganisationUserRole>();
        public OrganisationRole() : base() { }
    }
}