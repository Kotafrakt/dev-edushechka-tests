﻿using System.Collections.Generic;

namespace DevEdu.Tests.Models
{
    public class LessonInfoWithStudentsAndCommentsOutputModel : LessonInfoOutputModel
    {
        public List<CommentInfoOutputModel> Comments { get; set; }
        public List<StudentLessonOutputModel> Students { get; set; }

    }
}