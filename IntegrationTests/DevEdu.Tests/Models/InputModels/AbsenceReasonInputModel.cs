using System.ComponentModel.DataAnnotations;
using static DevEdu.Tests.Common.ValidationMessage;

namespace DevEdu.Tests.Models
{
    public class AbsenceReasonInputModel
    {
        [Required(ErrorMessage = AbsenceReasonRequired)]
        public string AbsenceReason { get; set; }
    }
}