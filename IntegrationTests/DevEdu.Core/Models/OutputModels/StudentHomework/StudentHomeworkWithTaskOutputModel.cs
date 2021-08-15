using System.Threading.Tasks;

namespace DevEdu.Core.Models
{
    public class StudentHomeworkWithTaskOutputModel
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public string CompletedDate { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public HomeworkInfoWithTaskOutputModel Homework { get; set; }
        public TagOutputModel Task { get; set; }
    }
}