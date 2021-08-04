using System.ComponentModel.DataAnnotations;
using DevEdu.Core.Common;
using static DevEdu.Core.Common.ValidationMessage;

namespace DevEdu.Core.Models
{
    public class GroupTaskInputModel
    {
        [Required(ErrorMessage = StartDateRequired)]
        [CustomDateFormatAttribute(ErrorMessage = WrongFormatDate)]
        public string StartDate { get; set; }
        [Required(ErrorMessage = EndDateRequired)]
        [CustomDateFormatAttribute(ErrorMessage = WrongFormatDate)]
        public string EndDate { get; set; }
    }
}