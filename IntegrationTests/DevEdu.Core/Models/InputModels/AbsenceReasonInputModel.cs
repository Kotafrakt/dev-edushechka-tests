using System.ComponentModel.DataAnnotations;
using static DevEdu.Core.Common.ValidationMessage;

namespace DevEdu.Core.Models
{
    public class AbsenceReasonInputModel
    {
        [Required(ErrorMessage = AbsenceReasonRequired)]
        public string AbsenceReason { get; set; }
    }
}