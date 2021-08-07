using System.Collections.Generic;

namespace DevEdu.Core.Models
{
    public class TaskInfoWithAnswersOutputModel : TaskInfoOutputModel
    {
        public List<StudentAnswerOnTaskOutputModel> StudentAnswers { get; set; }
    }
}