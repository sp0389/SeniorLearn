using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SeniorLearn.Areas.Member.Models.Lesson
{
    public class CreateLesson
    {
        [Required(ErrorMessage = "Lesson name is required.")]
        public string LessonName { get; set; } = default!;

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } = default!;

        [Required(ErrorMessage = "Please select a duration.")]
        public int Duration { get; set; }

        public List<SelectListItem> DurationOptions { get; set; } = new List<SelectListItem>();

        [Required(ErrorMessage = "Capacity is required.")]
        [Range(1, 100, ErrorMessage = "Capacity must be between 1 and 100.")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime StartDate { get; set; }

        // Optional: End date for recurring lessons or single lesson duration
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime? EndDate { get; set; }  // Nullable for non-recurring lessons

        [Required(ErrorMessage = "Please select a delivery mode.")]
        public string DeliveryMode { get; set; } = default!;

        public List<SelectListItem> DeliveryModes { get; set; } = new List<SelectListItem>();

        public string Location { get; set; } = default!;

        public string Frequency { get; set; } = "None";  // Default to 'None' for single lessons

        public List<SelectListItem> FrequencyOptions { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "None", Text = "None" },
            new SelectListItem { Value = "Daily", Text = "Daily" },
            new SelectListItem { Value = "Weekly", Text = "Weekly" }
        };

        public bool IsRecurring { get; set; }

        public int Occurrences { get; set; } = 1; // Default to 1 for non-recurring lessons

        // Weekly recurrence: Select specific days of the week
        public List<DayOfWeek> SelectedDaysOfWeek { get; set; } = new List<DayOfWeek>();

        public List<SelectListItem> DaysOfWeekOptions { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value = DayOfWeek.Sunday.ToString(), Text = "Sunday" },
            new SelectListItem { Value = DayOfWeek.Monday.ToString(), Text = "Monday" },
            new SelectListItem { Value = DayOfWeek.Tuesday.ToString(), Text = "Tuesday" },
            new SelectListItem { Value = DayOfWeek.Wednesday.ToString(), Text = "Wednesday" },
            new SelectListItem { Value = DayOfWeek.Thursday.ToString(), Text = "Thursday" },
            new SelectListItem { Value = DayOfWeek.Friday.ToString(), Text = "Friday" },
            new SelectListItem { Value = DayOfWeek.Saturday.ToString(), Text = "Saturday" }
        };

        public IEnumerable<SelectListItem> Courses { get; set; } = new List<SelectListItem>();

        public int? SelectedCourseId { get; set; }

        public bool IsCourse { get; set; }

        [Required(ErrorMessage = "Please select a status.")]
        public string Status { get; set; } = "Draft";

        public List<SelectListItem> StatusOptions { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Draft", Text = "Draft" },
            new SelectListItem { Value = "Scheduled", Text = "Scheduled" },
            new SelectListItem { Value = "Closed", Text = "Closed" },
            new SelectListItem { Value = "Complete", Text = "Complete" },
            new SelectListItem { Value = "Cancelled", Text = "Cancelled" }
        };
    }
}
