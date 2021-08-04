using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static DevEdu.Core.Common.ValidationMessage;

namespace DevEdu.Core.Models
{
    public class LessonInputModel
    {
        [Required(ErrorMessage = DateRequired)]
        public string Date { get; set; }

        [Required(ErrorMessage = TeacherCommentRequired)]
        public string TeacherComment { get; set; }

        [Required(ErrorMessage = TeacherIdRequired)]
        public int TeacherId { get; set; }

        [Url]
        public string LinkToRecord { get; set; }

        public List<int> TopicIds { get; set; }
    }
}