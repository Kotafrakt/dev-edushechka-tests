using System.ComponentModel.DataAnnotations;
using static DevEdu.Core.Common.ValidationMessage;

namespace DevEdu.Core.Models
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