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
    }

    public class OrganisationRole : IdentityRole
    {
        public ICollection<OrganisationUserRole> UserRoles { get; set; } = new List<OrganisationUserRole>();

        public OrganisationRole(string name) : base()
        {
            Name = name;
        }
    }
}
