using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeniorLearn.Models;
using SeniorLearn.Services;

namespace SeniorLearn.Controllers.Api
{
    [ApiController, Route("api/[controller]"), Authorize(Roles = "Administrator, Standard, Honorary, Professional")]
    public class UtilityController : ControllerBase
    {
        private readonly ApiService _apiService;

        public UtilityController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet, Route("member/enrolmentdates")]
        public async Task <IActionResult> EnrolmentDatesForCalendar()
        {
            var user = HttpContext.User.Identity!.Name;
            var dates = await _apiService.GetDatesForApiAsync(user!);
            return Ok(dates);
        }

        [HttpGet, Route("member/enrolments")]
        public async Task<IActionResult> UsersWithEnrolmentsCount()
        {
            var activeMemberEnrolments = await _apiService.GetActiveUserEnrolmentCountForApiAsync();
            var inactiveMemberEnrolments = await _apiService.GetInactiveUserEnrolmentCountForApiAsync();

            var enrolments = new List<int>
            {
                activeMemberEnrolments,
                inactiveMemberEnrolments,
            };
            return Ok(enrolments);
        }

        [AllowAnonymous]
        [HttpPost, Route("token")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            var token = string.Empty;
            if (ModelState.IsValid)
            {
                token = await _apiService.GetJwtTokenAsync(loginDto);
                if (token != "Invalid Credentials!")
                {
                    return Ok(token);  
                }
            }
            return BadRequest(token);
        }
    }
}