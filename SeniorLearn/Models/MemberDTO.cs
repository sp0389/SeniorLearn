namespace SeniorLearn.Models
{
    public class MemberDTO
    {
        public string Id { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? RenewalDate { get; set; }
    }
}