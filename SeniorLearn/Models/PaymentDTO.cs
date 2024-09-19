using SeniorLearn.Data;

namespace SeniorLearn.Models
{
    public class PaymentDTO
    {
        public int Id { get; set; }
        public DateTime? PaymentDate { get; set; }
        public PaymentType? PaymentType { get; set; }
        public decimal? PaymentAmount { get; set; }
    }
}