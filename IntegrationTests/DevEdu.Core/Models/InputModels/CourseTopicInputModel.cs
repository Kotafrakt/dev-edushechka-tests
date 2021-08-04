using DevEdu.Core.Common;
using System.ComponentModel.DataAnnotations;

namespace DevEdu.Core.Models
{
    public class CourseTopicInputModel
    {
        [Required(ErrorMessage = ValidationMessage.PositionRequired)]
        public int Position { get; set; }
    }
}