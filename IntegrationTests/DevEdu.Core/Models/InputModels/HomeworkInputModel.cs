using DevEdu.Core.Common;
using System.ComponentModel.DataAnnotations;
using static DevEdu.Core.Common.ValidationMessage;

namespace DevEdu.Core.Models
{
    public class HomeworkInputModel
    {
        [Required(ErrorMessage = StartDateRequired)]
        [CustomDateFormatAttribute(ErrorMessage = WrongFormatDate)]
        public string StartDate { get; set; }
        [Required(ErrorMessage = EndDateRequired)]
        [CustomDateFormatAttribute(ErrorMessage = WrongFormatDate)]
        public string EndDate { get; set; }
    }
}