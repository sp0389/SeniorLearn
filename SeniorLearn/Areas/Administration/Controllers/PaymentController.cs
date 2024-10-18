using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Mvc;
using SeniorLearn.Areas.Administration.Models.Payment;
using SeniorLearn.Data.Core;
using SeniorLearn.Models;
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
        public async Task<IActionResult> Index(string id, int pageNumber = 1, int pageSize = 10)
        {
            var member = await _organisationUserService.GetUserByIdAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            var memberPayments = await _paymentService.GetPaymentsAsync(member, (pageSize * pageNumber) - pageSize, pageSize);

            var pagedResult = new PagedResult<PaymentDTO>
            {
                Data  = memberPayments.ToList(),
                TotalItems = await _paymentService.GetPaymentsCountAsync(member),
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            return View(pagedResult);
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
                var member = await _organisationUserService.GetUserByIdAsync(id);

                if (member == null)
                {
                    return NotFound();
                }

                try
                {
                    await _paymentService.CreateNewPaymentAsync(member, p.PaymentDate!.Value, p.PaymentType!.Value, p.PaymentAmount!.Value);
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