using Microsoft.AspNetCore.Mvc.Rendering;
using SeniorLearn.Data;
using System.ComponentModel.DataAnnotations;

namespace SeniorLearn.Areas.Administration.Models.Member
{
    public class Edit : Register
    {
        [Required(ErrorMessage = "You must select a Role")]
        public RoleTypes? SelectedRole { get; set; }
        public int Duration { get; set; }
        public bool? RemoveRole { get; set; } = false;
        public string? Role { get; set; } = default!;
        public IEnumerable<SelectListItem> RoleTypes { get; set; } = new List<SelectListItem>();
        public IEnumerable<string> AssignedRoles { get; set; } = new List<string>();
    }
}