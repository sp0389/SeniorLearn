using Microsoft.AspNetCore.Mvc.Rendering;
using SeniorLearn.Data;
using System.ComponentModel.DataAnnotations;

namespace SeniorLearn.Areas.Administration.Models.Member
{
    public class EditViewModel : RegisterViewModel
    {
        [Required(ErrorMessage = "You must select a Role")]
        public RoleTypes? SelectedRole { get; set; }
        public int Duration { get; set; }
        public IEnumerable<SelectListItem> RoleTypes { get; set; } = new List<SelectListItem>();
        public IEnumerable<string> AssignedRoles { get; set; } = new List<string>();
    }
}