using System.ComponentModel.DataAnnotations;
using static DevEdu.Tests.Common.ValidationMessage;

namespace DevEdu.Tests.Models
{
    public class LessonUpdateInputModel
    {
        [Required(ErrorMessage = TeacherCommentRequired)]
        public string TeacherComment { get; set; }

        [Required(ErrorMessage = LinkToRecordIdRequired)]
        [Url]
        public string LinkToRecord { get; set; }

        [Required(ErrorMessage = DateRequired)]
        public string Date { get; set; }
    }
}