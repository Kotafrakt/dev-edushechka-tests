using System.Collections.Generic;

namespace DevEdu.Tests.Models
{
    public class LessonInfoWithCommentsOutputModel : LessonInfoOutputModel
    {
        public List<CommentInfoOutputModel> Comments { get; set; }
    }
}