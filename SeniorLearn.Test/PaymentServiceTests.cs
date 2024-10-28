using Microsoft.AspNetCore.Identity;
using SeniorLearn.Data;
using SeniorLearn.Data.Core;
using SeniorLearn.Services;

namespace SeniorLearn.Test
{
    public class PaymentServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly PaymentService _paymentService;
        private readonly OrganisationUserService _organisationUserService;

        public PaymentServiceTests(ApplicationDbContext context, PaymentService paymentService, UserManager<OrganisationUser> userManager
            , OrganisationUserService organisationUserService)
        {
            _context = context;
            _paymentService = paymentService;
            _organisationUserService = organisationUserService;
            _context.ChangeTracker.Clear();
        }

        [Fact]
        public async Task Should_LoadPaymentsForUser()
        {
            var m = await _organisationUserService.GetUserByUserNameAsync("a.admin@seniorlearn.com.au");
            var p = await _paymentService.GetPaymentsAsync(m);
            
            Assert.NotNull(p);        
        }

        [Fact]
        public async Task Should_GetPaymentsCountForUser()
        {
            var expectedCount = 1;
            var m = await _organisationUserService.GetUserByUserNameAsync("a.admin@seniorlearn.com.au");
            var p = await _paymentService.GetPaymentsCountAsync(m);

            Assert.True(expectedCount == p);
        }

        [Fact]
        public async Task Should_CreatePaymentRecordForUser()
        {
            var m = await _organisationUserService.GetUserByUserNameAsync("a.admin@seniorlearn.com.au");
            var p = m.CreateNewPaymentRecord(m, DateTime.UtcNow, PaymentType.Eft, 50.00m);

            Assert.NotNull(p);
            Assert.IsType<Payment>(p); 
        }
    }
}