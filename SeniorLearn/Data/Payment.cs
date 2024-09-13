using System.ComponentModel.DataAnnotations;

namespace SeniorLearn.Data
{
    public enum PaymentType
    {
        Cheque,
        Cash,
        [Display(Name = "Electronic Funds Transfer")]
        Eft,
        [Display(Name = "Credit Card")]
        CreditCard
    }

    public class Payment
    {
        public int Id { get; set; }
        public string UserId { get; set; } = default!;
        public OrganisationUser User { get; set; } = default!;
        public DateTime PaymentDate { get; set; }
        public PaymentType PaymentType { get; set; }
        private Payment() { }
        public Payment(OrganisationUser user, DateTime paymentDate, PaymentType paymentType)
        {
            UserId = user.Id;
            User = user;
            PaymentDate = paymentDate;
            PaymentType = paymentType;
        }
    }
}