using SeniorLearn.Data;
using System.ComponentModel.DataAnnotations;

namespace SeniorLearn.Areas.Administration.Models.Payment
{
    public class PaymentViewModel
    {
        [Required]
        public DateTime PaymentDate { get; set; }
        [Required]
        public PaymentType PaymentType { get; set; }
        [Required]
        public decimal PaymentAmount { get; set; }
    }
}