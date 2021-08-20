using DevEdu.Core.Models;
using DevEdu.Tests.Data;

namespace DevEdu.Tests.Creators
{
    public class StudentHomeworkCreator : BaseCreator
    {
        public StudentHomeworkWithHomeworkOutputModel AddStudentHomework(string token, int homeworkId)
        {
            var model = new StudentHomeworkInputModel();
            return new StudentHomeworkWithHomeworkOutputModel();
        }

        public StudentHomeworkWithHomeworkOutputModel UpdateStudentHomework(string token, int homeworkId)
        {
            var model = new StudentHomeworkInputModel();
            return new StudentHomeworkWithHomeworkOutputModel();
        }

        public StudentHomeworkWithHomeworkOutputModel UpdateStatusOfStudentHomework(string token, int homeworkId, int statusId)
        {
            return new StudentHomeworkWithHomeworkOutputModel();
        }
    }
}