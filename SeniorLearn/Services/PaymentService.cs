using SeniorLearn.Data;
using SeniorLearn.Data.Core;

namespace SeniorLearn.Services
{
    public class PaymentService
    {
        private readonly ApplicationDbContext _context;

        public PaymentService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Payment> CreateNewPaymentAsync(OrganisationUser user, DateTime paymentDate, PaymentType paymentType)
        {
            var payment = new Payment(user, paymentDate, paymentType)
            {
                UserId = user.Id,
                User = user,
                PaymentDate = paymentDate,
                PaymentType = paymentType
            };

            await _context.AddAsync(payment);
            await _context.SaveChangesAsync();
            return payment;
        }
    }
}
