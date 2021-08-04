using System.ComponentModel.DataAnnotations;
using static DevEdu.Core.Common.ValidationMessage;

namespace DevEdu.Core.Models
{
    public class AttendanceInputModel
    {
        [Required(ErrorMessage = AttendanceRequired)]
        public bool IsPresent { get; set; }
    }
}