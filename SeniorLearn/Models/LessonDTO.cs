using Microsoft.AspNetCore.Mvc.Rendering;
using SeniorLearn.Data;

namespace SeniorLearn.Models
{
    public class LessonDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Address { get; set; } = default!;
        public int Duration { get; set; }
        public string Type { get; set; } = default!;
        public bool IsInCourse { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid GroupId { get; set; }
        public Availability Availability { get; set; }
        public IEnumerable<SelectListItem> StateList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Text = "Scheduled", Value = "Scheduled"},
            new SelectListItem { Text = "Cancelled", Value = "Cancelled"},
            new SelectListItem { Text = "Closed", Value = "Closed"},
            new SelectListItem { Text = "Complete", Value = "Complete"},

        };
    }
}