namespace SeniorLearn.Data
{
    public class Organisation
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public ICollection<OrganisationUser> Users { get; set; } = new List<OrganisationUser>();
        public Organisation() { }

        public Member RegisterMember(string firstName, string lastName, string email)
        {
            var member = new Member(Id, email, firstName, lastName, email)
            {
                Registered = DateTime.UtcNow,
            };

            Users.Add(member);
            return member;
        }
    }
}