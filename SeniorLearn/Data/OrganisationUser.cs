using Microsoft.AspNetCore.Identity;

namespace SeniorLearn.Data
{
    public abstract class OrganisationUser : IdentityUser
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTime Registered { get; set; }
        public int OrganisationId { get; set; }
        public Organisation Organisation { get; set; } = default!;
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
        protected Member() { }
        public Member(int organisationId, string username, string firstName, string lastName, string email) :
            base(organisationId, username, firstName, lastName, email)
        {
        }
    }
}
