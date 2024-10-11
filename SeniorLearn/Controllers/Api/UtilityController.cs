using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task <IActionResult> Index()
        {
            var user = HttpContext.User.Identity!.Name;
            var dates = await _apiService.GetDatesForApiAsync(user!);
            return Ok(dates);
        }
    }
}