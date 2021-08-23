using DevEdu.Core.Models;
using DevEdu.Tests.Creators;
using System.Collections.Generic;

namespace DevEdu.Tests.Facades
{
    public class TaskFacade
    {
        private TaskCreator _creator;
        public TaskFacade() { _creator = new TaskCreator(); }

        internal TaskInfoOutputModel CreateCorrectTaskByTeacherWithoutHomework(string token, List<int> tagIds = default)
        {
            return _creator.AddTaskByTeacherWithoutHomework(token, tagIds);
        }
    }
}