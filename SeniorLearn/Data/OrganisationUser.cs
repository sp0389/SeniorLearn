using Microsoft.AspNetCore.Identity;
namespace SeniorLearn.Data
{
    public enum Status
    {
        Inactive,
        Active,
    }
    public abstract class OrganisationUser : IdentityUser
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTime Registered { get; set; }
        public byte[] Version { get; set;} = default!;
        public int OrganisationId { get; set; }
        public Status Status { get; set; }
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
        public abstract OrganisationUserRole GrantStandardRole(OrganisationUser user, OrganisationRole role, DateTime startDate, DateTime? renewalDate);
        public abstract OrganisationUserRole GrantProfessionalRole(OrganisationUser user, OrganisationRole role, DateTime startDate, int duration, DateTime? renewalDate);
        public abstract OrganisationUserRole GrantHonoraryRole(OrganisationUser user, OrganisationRole role, DateTime startDate);
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

        public override OrganisationUserRole GrantStandardRole(OrganisationUser user, OrganisationRole role, DateTime startDate, DateTime? renewalDate)
        {
            var standard = new OrganisationUserRole(user, role)
            {
                StartDate = new DateTime(startDate.Year, startDate.Month, 1).AddMonths(1),
            };
            standard.EndDate = renewalDate ?? standard.StartDate.AddYears(1);
            standard.RoleType = RoleTypes.Standard;
            user.Status = Status.Active;
            return standard;
        }

        public override OrganisationUserRole GrantProfessionalRole(OrganisationUser user, OrganisationRole role, DateTime startDate, int duration, DateTime? renewalDate)
        {
            var professional = new OrganisationUserRole(user, role) 
            {
                StartDate = new DateTime(startDate.Year, startDate.Month, 1).AddMonths(1),
            };

            if (renewalDate.HasValue)
            {
                professional.EndDate = renewalDate.Value;
            }
            else
            {
                professional.EndDate = duration == 3 ? professional.StartDate.AddMonths(3) : professional.StartDate.AddYears(1);
            }
            professional.RoleType = RoleTypes.Professional;
            user.Status = Status.Active;
            return professional;
        }

        public override OrganisationUserRole GrantHonoraryRole(OrganisationUser user, OrganisationRole role, DateTime startDate)
        {
            var honorary = new OrganisationUserRole(user, role)
            {
                StartDate = new DateTime(startDate.Year, startDate.Month, 1).AddMonths(1),
                EndDate = DateTime.MaxValue,
                RoleType = RoleTypes.Honorary,
            };
            user.Status = Status.Active;
            return honorary;
        }
    }
}