using System.ComponentModel.DataAnnotations;
using DevEdu.Tests.Common;

namespace DevEdu.Tests.Models
{
    public class CourseTopicInputModel
    {
        [Required(ErrorMessage = ValidationMessage.PositionRequired)]
        public int Position { get; set; }
    }
}