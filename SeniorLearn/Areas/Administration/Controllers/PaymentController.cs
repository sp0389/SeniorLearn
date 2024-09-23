using Microsoft.AspNetCore.Mvc;
using SeniorLearn.Areas.Administration.Models.Payment;
using SeniorLearn.Controllers;
using SeniorLearn.Data.Core;
using SeniorLearn.Services;

namespace SeniorLearn.Areas.Administration.Controllers
{
    public class PaymentController : AdministrationController
    {
        private readonly OrganisationUserService _organisationUserService;
        private readonly PaymentService _paymentService;

        public PaymentController(ApplicationDbContext context, ILogger<MemberController> logger, 
            OrganisationUserService organisationUserService, PaymentService paymentService)
            : base(context, logger)
        {
            _organisationUserService = organisationUserService;
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            var user = await _organisationUserService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userPayments = await _paymentService.GetPaymentsAsync(user);
            return View(userPayments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var currentDateTime = new Create()
            {
                PaymentDate = DateTime.UtcNow,
            };

            return View(currentDateTime);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(string id, Create p)
        {
            if (ModelState.IsValid)
            {
                var user = await _organisationUserService.GetUserByIdAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                try
                {
                    await _paymentService.CreateNewPaymentAsync(user, p.PaymentDate!.Value, p.PaymentType!.Value, p.PaymentAmount!.Value);
                    return RedirectToAction("Index", new { id });
                }
                
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    _logger.LogError(ex, ex.Message);
                }
            }
            return View(p);
        }
    }
}