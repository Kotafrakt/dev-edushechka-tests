using DevEdu.Core.Models;
using DevEdu.Tests.Creators;
using System.Collections.Generic;

namespace DevEdu.Tests.Facades
{
    public class TaskFacade
    {
        private TaskCreator _creator;
        public TaskFacade() { _creator = new TaskCreator(); }

        internal TaskInfoOutputModel CreateValidTaskByTeacherWithoutHomework(string token, List<int> tagIds = default)
        {
            return _creator.AddTaskByTeacherWithoutHomework(token, tagIds);
        }
        internal TaskInfoOutputModel CreateValidTaskByTeacherWithHomework(string token, int groupId, List<int> tagIds = default)
        {
            return _creator.AddTaskByTeacherWithHomework(token, groupId, tagIds);
        }
        internal TaskInfoOutputModel CreateValidTaskByMethodist(string token, List<int> courseIds = default, List<int> tagIds = default)
        {
            return _creator.AddTaskByMethodist(token, courseIds, tagIds);
        }
    }
}