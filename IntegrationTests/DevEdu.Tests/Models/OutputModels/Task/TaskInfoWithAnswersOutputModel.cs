using System.Collections.Generic;

namespace DevEdu.Tests.Models
{
    public class TaskInfoWithAnswersOutputModel : TaskInfoOutputModel
    {
        public List<StudentAnswerOnTaskInfoOutputModel> StudentAnswers { get; set; }
    }
}