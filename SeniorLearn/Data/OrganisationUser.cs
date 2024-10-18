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

        protected OrganisationUser() { }

        public OrganisationUser(int organisationId, string username, string firstName, string lastName, string email)
        {
            OrganisationId = organisationId;
            UserName = username;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }

    public class Member : OrganisationUser
    {
        public ICollection<Enrolment> Enrolments { get; set; } = new List<Enrolment>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public Member() { }

        public Member(int organisationId, string username, string firstName, string lastName, string email) :
            base(organisationId, username, firstName, lastName, email) { }

        public Payment CreateNewPaymentRecord(Member member, DateTime paymentDate, PaymentType paymentType, decimal paymentAmount)
        {
            var payment = new Payment(member, paymentDate, paymentType, paymentAmount);
            return payment;
        }

        public OrganisationUserRole GrantStandardRole(Member member, OrganisationRole role, DateTime startDate, DateTime? renewalDate)
        {
            var standard = new OrganisationUserRole(member, role)
            {
                StartDate = new DateTime(startDate.Year, startDate.Month, 1).AddMonths(1),
            };
            standard.EndDate = renewalDate ?? standard.StartDate.AddYears(1);
            standard.RoleType = RoleTypes.Standard;
            member.Status = Status.Active;
            return standard;
        }

        public OrganisationUserRole GrantProfessionalRole(Member member, OrganisationRole role, DateTime startDate, int duration, DateTime? renewalDate)
        {
            var professional = new OrganisationUserRole(member, role) 
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
            member.Status = Status.Active;
            return professional;
        }

        public OrganisationUserRole GrantHonoraryRole(Member member, OrganisationRole role, DateTime startDate)
        {
            var honorary = new OrganisationUserRole(member, role)
            {
                StartDate = new DateTime(startDate.Year, startDate.Month, 1).AddMonths(1),
                EndDate = DateTime.MaxValue,
                RoleType = RoleTypes.Honorary,
            };
            member.Status = Status.Active;
            return honorary;
        }
    }
}