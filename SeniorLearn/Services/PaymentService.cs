using Microsoft.EntityFrameworkCore;
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

        public async Task<Payment> CreateNewPaymentAsync(OrganisationUser user, DateTime? paymentDate, PaymentType? paymentType, decimal? paymentAmount)
        {
            var payment = new Payment(user, paymentDate, paymentType, paymentAmount)
            {
                UserId = user.Id,
                User = user,
                PaymentDate = paymentDate,
                PaymentType = paymentType,
                PaymentAmount = paymentAmount
            };

            await _context.AddAsync(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task<IEnumerable<Payment>> GetPaymentsAsync(OrganisationUser user)
        {
            var payments = await _context.Payments.Where(p => p.UserId == user.Id).ToListAsync();
            return payments;
        }
    }
}
