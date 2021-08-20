using DevEdu.Core.Models;
using DevEdu.Tests.Data;

namespace DevEdu.Tests.Creators
{
    public class TaskCreator : BaseCreator
    {
        public TaskInfoOutputModel AddTaskByTeacher(string token)
        {
            var model = new TaskByTeacherInputModel();
            return new TaskInfoOutputModel();
        }

        public TaskInfoOutputModel AddTaskByMethodist(string token)
        {
            var model = new TaskByMethodistInputModel();
            return new TaskInfoOutputModel();
        }

        public TaskInfoOutputModel UpdateTaskByTeacher(string token, int taskId)
        {
            var model = new TaskByTeacherUpdateInputModel();
            return new TaskInfoOutputModel();
        }

        public TaskInfoOutputModel UpdateTaskByMethodist(string token, int taskId)
        {
            var model = "";//new TaskInputModel();
            return new TaskInfoOutputModel();
        }

        public void AddTagToTask(string token, int taskId, int tagId)
        {
        }
    }
}