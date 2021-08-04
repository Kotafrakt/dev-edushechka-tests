using System.Collections.Generic;

namespace DevEdu.Core.Models
{
    public class LessonInfoWithCommentsOutputModel : LessonInfoOutputModel
    {
        public List<CommentInfoOutputModel> Comments { get; set; }
    }
}