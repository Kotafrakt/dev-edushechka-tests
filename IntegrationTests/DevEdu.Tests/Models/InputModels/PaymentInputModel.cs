using DevEdu.Tests.Common;
using System;
using System.ComponentModel.DataAnnotations;
using static DevEdu.Tests.Common.ValidationMessage;


namespace DevEdu.Tests.Models
{
    public class PaymentInputModel
    {
        [Required(ErrorMessage = DateRequired)]
        [CustomDateFormatAttribute(ErrorMessage = WrongFormatDate)]
        public string Date { get; set; }

        [Required(ErrorMessage = SumRequired)]
        public int Sum { get; set; }

        [Required(ErrorMessage = UserIdRequired)]
        public int UserId { get; set; }

        [Required(ErrorMessage = IsPaidRequired)]
        public int IsPaid { get; set; }
    }
}