using SeniorLearn.Data;

namespace SeniorLearn.Models
{
    public class EnrolmentDTO
    {
        public int Id { get; set; }
        public string MemberId { get; set; } = default!;
        public Member Member { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid GroupId { get; set; }
        public Course Course { get; set; } = default!;
        public int Duration { get; set; } = default!;
        public string Address { get; set; } = default!;
        public bool IsInCourse { get; set; }
    }
}
