using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SeniorLearn.Data;
using SeniorLearn.Services;

namespace SeniorLearn.Controllers.Api
{
    [Authorize(AuthenticationSchemes = "JWT_OR_COOKIE", Roles = "Administrator, Standard, Honorary, Professional")]
    [ApiController, Route("api/[controller]")]
    public class BulletinController : ControllerBase
    {
        private readonly BulletinService _bulletinService;

        public BulletinController(BulletinService bulletinService)
        {
            _bulletinService = bulletinService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBulletins()
        {
            var result = await _bulletinService.GetBulletinsAsync();

            if (result.IsNullOrEmpty())
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBulletinsById([FromRoute] string id)
        {
            var result = await _bulletinService.GetBulletinByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetBulletinsBySearch([FromQuery] string searchTerm)
        {
            var result = await _bulletinService.GetBulletinsBySearchTermAsync(searchTerm);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewBulletin([FromForm] string title, [FromForm] string contentMessage, [FromForm] string? tags, [FromForm] IFormFile? image)
        {
            var tagList = new List<string>();
            if (ModelState.IsValid)
            {
                try
                {
                    if (tags != null)
                    {
                        tagList = tags.Split(',').ToList();
                    }
                    var member = HttpContext.User.Identity!.Name;
                    var bulletin = await _bulletinService.SaveNewBulletinAsync(title, contentMessage, tagList, image, member!);
                    return CreatedAtAction(nameof(GetBulletinsById), new { id = bulletin.Id }, bulletin);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBulletin([FromRoute] string id, [FromForm] string title, [FromForm] string contentMessage,
            [FromForm] string contentImageUrl, [FromForm] string status, [FromForm] string? tags, [FromForm] IFormFile? image)
        {
            var tagList = new List<string>();
            if (ModelState.IsValid)
            {
                try
                {
                    if (tags != null)
                    {
                        tagList = tags.Split(',').ToList();
                    }
                    var member = HttpContext.User.Identity!.Name;
                    var result = await _bulletinService.UpdateExistingBulletinAsync(id, title, contentMessage, contentImageUrl, status, tagList, image);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPut("comment/{id}")]
        public async Task<IActionResult> AddCommentToBulletin([FromRoute]string id, [FromBody] BulletinComment bulletinComment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var member = HttpContext.User.Identity!.Name;
                    var result = await _bulletinService.AddCommentToBulletinAsync(id, bulletinComment, member!);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task <IActionResult> DeleteBulletinById([FromRoute]string id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _bulletinService.DeleteBulletinAsync(id);
                    return Ok();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateBulletinLikes([FromRoute]string id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _bulletinService.UpdateBulletinLikesAsync(id);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPatch("{id}/unlike")]
        public async Task<IActionResult> UnlikeBulletin(string id)
        {
            try
            {
                var bulletin = await _bulletinService.DecreaseBulletinLikesAsync(id);
                return Ok(bulletin);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}