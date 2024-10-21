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
        public DateTime RecurringStartDate { get; set; }
        [Required(ErrorMessage = "Start date is required.")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime SingleStartDate { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime? EndDate { get; set; }
        [Required(ErrorMessage = "Please select a delivery mode.")]
        public string DeliveryMode { get; set; } = default!;
        public List<SelectListItem> DeliveryModes { get; set; } = new List<SelectListItem>();
        public string Location { get; set; } = default!;
        public bool IsRecurring { get; set; }
        public int Occurrences { get; set; } = 0;
        public IList<DayOfWeek> SelectedDaysOfWeek { get; set; } = new List<DayOfWeek>();
        public IEnumerable<SelectListItem> Courses { get; set; } = new List<SelectListItem>();
        public int? SelectedCourseId { get; set; }
        public bool IsCourse { get; set; }
    }
}
