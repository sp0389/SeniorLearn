namespace SeniorLearn.Models
{
    public class CalendarDTO
    {
        public string Title { get; set; } = default!;
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Address { get; set; } = default!;
        public string Lecturer { get; set; } = default!;
    }
}