using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeniorLearn.Areas.Member.Models.Lesson;
using SeniorLearn.Data.Core;
using SeniorLearn.Services;
using SeniorLearn.Controllers;

namespace SeniorLearn.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Professional")]
    public class LessonController : BaseController
    {
        private readonly LessonService _lessonService;

        public LessonController(ApplicationDbContext context, ILogger<LessonController> logger, LessonService lessonService)
            : base(context, logger)
        {
            _lessonService = lessonService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var lessonModel = new CreateLesson
            {
                RecurringStartDate = DateTime.UtcNow,
                SingleStartDate = DateTime.UtcNow,
            };
            await _lessonService.PopulateLessonDropdownsAsync(lessonModel);
            return View(lessonModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(CreateLesson model)
        {
            if (ModelState.IsValid)
            {
                if (model.IsRecurring)
                {
                    try
                    {
                        var member = HttpContext.User.Identity!.Name;
                        await _lessonService.CreateLessonAsync(model, member!);
                        return RedirectToAction("Index");
                    }
                    catch (DomainRuleException ex)
                    {
                        ModelState.AddModelError("", ex.Message);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", ex.Message);
                    }
                }
                else
                {
                    model.EndDate = null;
                    try
                    {
                        var member = HttpContext.User.Identity!.Name;
                        await _lessonService.CreateLessonAsync(model, member!);
                        return RedirectToAction("Index");
                    }
                    catch (DomainRuleException ex)
                    {
                        ModelState.AddModelError("", ex.Message);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", ex.Message);
                    }
                }
            }
            await _lessonService.PopulateLessonDropdownsAsync(model);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var lessons = await _lessonService.GetLessonsForCalendarAsync(); 
            return View(lessons);
        }

        [HttpGet]
        public async Task<IActionResult> Calendar()
        {
            var lessons = await _lessonService.GetLessonsForCalendarAsync();
            return View(lessons); 
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var lessonDetails = await _lessonService.GetLessonDetailsAsync(id);
                return View(lessonDetails);
            }
            catch (DomainRuleException ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]

        public async Task<IActionResult> UpdateLessonState(IList<int> Lessons, string lessonState)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _lessonService.UpdateLessonStateAsync(Lessons, lessonState);
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }
            return RedirectToAction("Index");
        }
    }
}
