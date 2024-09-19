namespace SeniorLearn.Data
{
    public class Organisation
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public ICollection<OrganisationUser> Users { get; set; } = new List<OrganisationUser>();
        public Organisation() { }
    }
}