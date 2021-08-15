using DevEdu.Core.Common;
using System.ComponentModel.DataAnnotations;

namespace DevEdu.Core.Models
{
    public class StudentHomeworkInputModel
    {
        [Required(ErrorMessage = ValidationMessage.StudentAnswerRequired)]
        public string Answer { get; set; }
    }
}