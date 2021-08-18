using DevEdu.Core.Models;
using DevEdu.Tests.Data;

namespace DevEdu.Tests.Creators
{
    public class HomeworkCreator : BaseCreator
    {
        public HomeworkInfoOutputModel AddHomework(string token, int groupId, int taskId)
        {
            var model = new HomeworkInputModel();
            return new HomeworkInfoOutputModel();
        }

        public HomeworkInfoOutputModel UpdateHomework(string token, int homeworkId)
        {
            var model = new HomeworkInputModel();
            return new HomeworkInfoOutputModel();
        }
    }
}