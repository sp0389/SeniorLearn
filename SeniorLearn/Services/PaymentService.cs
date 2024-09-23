using Mapster;
using Microsoft.EntityFrameworkCore;
using SeniorLearn.Data;
using SeniorLearn.Data.Core;
using SeniorLearn.Models;

namespace SeniorLearn.Services
{
    public class PaymentService
    {
        private readonly ApplicationDbContext _context;

        public PaymentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaymentDTO> CreateNewPaymentAsync(OrganisationUser user, DateTime paymentDate, PaymentType paymentType, decimal paymentAmount)
        {
            var payment = user.CreateNewPaymentRecord(user, paymentDate, paymentType, paymentAmount);

            await _context.AddAsync(payment);
            await _context.SaveChangesAsync();
            return payment.Adapt<PaymentDTO>();
        }

        public async Task<IEnumerable<PaymentDTO>> GetPaymentsAsync(OrganisationUser user)
        {
            return await _context.Payments.Where(p => p.UserId == user.Id).ProjectToType<PaymentDTO>().ToListAsync();
        }
    }
}