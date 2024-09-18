using System.ComponentModel.DataAnnotations;

namespace SeniorLearn.Areas.Administration.Models.Member
{
    public class Register
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is Required.")]
        public string? FirstName { get; set; } = default!;
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is Required.")]
        public string? LastName { get; set; } = default!;
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is Required.")]
        public string? Email { get; set; } = default!;
    }
}