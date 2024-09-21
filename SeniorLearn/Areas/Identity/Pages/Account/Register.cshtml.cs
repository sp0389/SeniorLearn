using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SeniorLearn.Data.Core;
using SeniorLearn.Data;
using SeniorLearn.Services;
using System.ComponentModel.DataAnnotations;
public class RegisterModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly OrganisationUserService _organisationService;
    private readonly SignInManager<OrganisationUser> _signInManager;

    public RegisterModel(ApplicationDbContext context, OrganisationUserService organisationService, SignInManager<OrganisationUser> signInManager)
    {
        _context = context;
        _organisationService = organisationService;
        _signInManager = signInManager;
    }
    [BindProperty]
    public InputModel Input { get; set; } = default!;
    public class InputModel
    {
        [Required, Display(Name = "Organisation")]
        public int OrganisationId { get; set; } = 1;
        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; } = default!;
        [Required, Display(Name = "Last Name")]
        public string LastName { get; set; } = default!;
        [Required, Display(Name = "Email")]
        public string Email { get; set; } = default!;
        //TODO: Add error message
        //[DataType(DataType.Password), Required, StringLength(100, ErrorMessage = "")]
        public string Password { get; set; } = default!;
        //TODO: Add error message
        //[DataType(DataType.Password),Display(Name = "Confirm"), Compare("Password", ErrorMessage = "")]
        public string ConfirmPassword { get; set; } = default!;
    }
    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            try
            {
                var member = await _organisationService.RegisterMemberAsync(Input.OrganisationId,
                    Input.FirstName, Input.LastName, Input.Email, Input.Password);
                await _signInManager.SignInAsync(await _context.Users.FirstAsync(user => user.Id == member.Id), false);

                // TODO: Redirect to somewhere relevant later when I implement it like a members area.
                return LocalRedirect("/");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
        }
        return Page();
    }
}