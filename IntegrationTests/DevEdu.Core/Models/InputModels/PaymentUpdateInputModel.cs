using DevEdu.Core.Common;
using System.ComponentModel.DataAnnotations;
using static DevEdu.Core.Common.ValidationMessage;

namespace DevEdu.Core.Models
{
    public class PaymentUpdateInputModel
    {
        [Required(ErrorMessage = DateRequired)]
        [CustomDateFormatAttribute(ErrorMessage = WrongFormatDate)]
        public string Date { get; set; }

        [Required(ErrorMessage = SumRequired)]
        public int Sum { get; set; }

        [Required(ErrorMessage = IsPaidRequired)]
        public int IsPaid { get; set; }
    }
}