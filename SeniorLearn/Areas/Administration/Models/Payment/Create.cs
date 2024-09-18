using SeniorLearn.Data;
using System.ComponentModel.DataAnnotations;

namespace SeniorLearn.Areas.Administration.Models.Payment
{
    public class Create
    {
        [DataType(DataType.DateTime)]
        [Display(Name = "Payment Date")]
        [Required(ErrorMessage = "You must select a date.")]
        public DateTime? PaymentDate { get; set; }
        [Display(Name = "Payment Type")]
        [Required(ErrorMessage = "You must select a payment type.")]
        public PaymentType? PaymentType { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Payment Amount")]
        [Required(ErrorMessage = "You must provide a payment amount.")]
        public decimal? PaymentAmount { get; set; }
    }
}