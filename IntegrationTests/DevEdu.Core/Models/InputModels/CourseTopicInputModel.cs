using System.ComponentModel.DataAnnotations;
using DevEdu.Core.Common;

namespace DevEdu.Core.Models
{
    public class CourseTopicInputModel
    {
        [Required(ErrorMessage = ValidationMessage.PositionRequired)]
        public int Position { get; set; }
    }
}