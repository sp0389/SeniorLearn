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

        public async Task<Payment> CreateNewPaymentAsync(Member member, DateTime paymentDate, PaymentType paymentType, decimal paymentAmount)
        {
            var payment = member.CreateNewPaymentRecord(member, paymentDate, paymentType, paymentAmount);

            await _context.AddAsync(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task<IEnumerable<PaymentDTO>> GetPaymentsAsync(Member member, int skip = 0, int take = int.MaxValue)
        {
            return await _context.Payments.Where(p => p.MemberId == member.Id).Skip(skip).Take(take)
                .ProjectToType<PaymentDTO>()
                .ToListAsync();
        }

        public async Task<int> GetPaymentsCountAsync(Member member)
        {
            return await _context.Payments.Where(p => p.MemberId == member.Id)
                .CountAsync();
        }
    }
}