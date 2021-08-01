using System.ComponentModel.DataAnnotations;
using static DevEdu.Tests.Common.ValidationMessage;

namespace DevEdu.Tests.Models
{
    public class AttendanceInputModel
    {
        [Required(ErrorMessage = AttendanceRequired)]
        public bool IsPresent { get; set; }
    }
}