namespace DevEdu.Core.Models
{
    public class StudentHomeworkWithHomeworkOutputModel : StudentHomeworkOutputModel
    {
        public HomeworkInfoWithTaskOutputModel Homework { get; set; }
    }
}