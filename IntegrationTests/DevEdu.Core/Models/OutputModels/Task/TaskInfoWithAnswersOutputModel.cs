using System.Collections.Generic;

namespace DevEdu.Core.Models
{
    public class TaskInfoWithAnswersOutputModel : TaskInfoOutputModel
    {
        public List<StudentHomeworkOutputModel> StudentAnswers { get; set; }
    }
}