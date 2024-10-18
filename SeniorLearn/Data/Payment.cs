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
        public string MemberId { get; set; } = default!;
        public Member Member { get; set; } = default!;
        public DateTime PaymentDate { get; set; }
        public PaymentType PaymentType { get; set; }
        public decimal PaymentAmount { get; set; }
        private Payment() { }
        public Payment(Member member, DateTime paymentDate, PaymentType paymentType, decimal paymentAmount)
        {
            MemberId = member.Id;
            Member = member;
            PaymentDate = paymentDate;
            PaymentType = paymentType;
            PaymentAmount = paymentAmount;
        }
    }
}