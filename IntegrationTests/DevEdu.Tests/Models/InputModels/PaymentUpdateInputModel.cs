using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static DevEdu.Tests.Common.ValidationMessage;
using System.Linq;
using System.Threading.Tasks;
using DevEdu.Tests.Common;

namespace DevEdu.Tests.Models
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
