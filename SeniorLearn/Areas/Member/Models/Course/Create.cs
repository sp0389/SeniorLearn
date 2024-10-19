using System.ComponentModel.DataAnnotations;

namespace SeniorLearn.Areas.Member.Models.Course
{
    public class Create
    {
        [Required(ErrorMessage = "Course name is required.")]
        public string CourseName { get; set; } = default!;
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } = default!;
        [Required(ErrorMessage = "Start date is required.")]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End date is required.")]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }
    }
}