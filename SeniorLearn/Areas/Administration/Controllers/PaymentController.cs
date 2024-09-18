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
        public async Task<IActionResult> Index(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

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
                var user = await _userManager.FindByIdAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                try
                {
                    await _paymentService.CreateNewPaymentAsync(user, p.PaymentDate, p.PaymentType, p.PaymentAmount);
                    return RedirectToAction("Index", new { id });
                }

                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(p);
        }
    }
}