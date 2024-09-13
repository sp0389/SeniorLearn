using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SeniorLearn.Areas.Administration.Models.Payment;
using SeniorLearn.Data;
using SeniorLearn.Data.Core;
using SeniorLearn.Services;

namespace SeniorLearn.Areas.Administration.Controllers
{
    public class PaymentController : AdministrationController
    {
        private readonly UserManager<OrganisationUser> _userManager;
        private readonly PaymentService _paymentService;

        public PaymentController(ApplicationDbContext context, ILogger<MemberController> logger, UserManager<OrganisationUser> userManager, PaymentService paymentService)
            : base(context, logger)
        {
            _userManager = userManager;
            _paymentService = paymentService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string id, PaymentViewModel p)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                try
                {
                    await _paymentService.CreateNewPaymentAsync(user, p.PaymentDate, p.PaymentType);
                }

                catch (DomainRuleException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return RedirectToAction("Index", new { id });
        }
    }
}