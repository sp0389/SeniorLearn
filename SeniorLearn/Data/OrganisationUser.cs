using Microsoft.AspNetCore.Identity;

namespace SeniorLearn.Data
{
    public abstract class OrganisationUser : IdentityUser
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTime Registered { get; set; }
        public byte[] Version { get; set;} = default!;
        public int OrganisationId { get; set; }
        public Organisation Organisation { get; set; } = default!;
        public ICollection<OrganisationUserRole> UserRoles { get; set; } = new List<OrganisationUserRole>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<Enrolment> Enrolments { get; set; } = new List<Enrolment>();

        protected OrganisationUser() { }

        public OrganisationUser(int organisationId, string username, string firstName, string lastName, string email)
        {
            OrganisationId = organisationId;
            UserName = username;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public abstract Payment CreateNewPaymentRecord(OrganisationUser user, DateTime paymentDate, PaymentType paymentType, decimal paymentAmount);
        public abstract void AssignRoleToMember(RoleTypes? roleType, OrganisationUserRole assignUserRole, DateTime startDate,
            DateTime? renewalDate, int duration);
    }

    public class Member : OrganisationUser
    {
        public Member() { }
        public Member(int organisationId, string username, string firstName, string lastName, string email) :
            base(organisationId, username, firstName, lastName, email) { }

        public override Payment CreateNewPaymentRecord(OrganisationUser user, DateTime paymentDate, PaymentType paymentType, decimal paymentAmount)
        {
            var payment = new Payment(user, paymentDate, paymentType, paymentAmount);
            return payment;
        }

        public override void AssignRoleToMember(RoleTypes? roleType, OrganisationUserRole assignUserRole, DateTime startDate,
            DateTime? renewalDate, int duration)
        {
            switch (roleType)
            {
                case RoleTypes.Standard:
                    assignUserRole.GrantStandardRole(startDate, renewalDate);
                    break;
                case RoleTypes.Professional:
                    assignUserRole.GrantProfessionalRole(startDate, duration, renewalDate);
                    break;
                case RoleTypes.Honorary:
                    assignUserRole.GrantHonoraryRole();
                    break;
            }
        }
    }
}
