using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Constants;
using DevEdu.Tests.Data;
using System.Collections.Generic;

namespace DevEdu.Tests.Creators
{
    public class TaskCreator : BaseCreator
    {
        public TaskInfoOutputModel AddTaskByTeacherWithoutHomework(string token, List<int> tagIds = default)
        {
            _endPoint = TaskEndpoints.AddTaskByTeacherEndpoint;
            var postData = TaskData.GetValidTaskByTeacherWithoutHomework(tagIds);
            var request = _requestHelper.CreatePostRequest(_endPoint, postData, token);
            var response = _client.Execute<TaskInfoOutputModel>(request);
            var result = response.Data;
            return result;
        }

        public TaskInfoOutputModel AddTaskByTeacherWithHomework(string token, int groupId, List<int> tagIds = default)
        {
            _endPoint = TaskEndpoints.AddTaskByTeacherEndpoint;
            var postData = TaskData.GetValidTaskByTeacherWithHomework(groupId, tagIds);
            var request = _requestHelper.CreatePostRequest(_endPoint, postData, token);
            var response = _client.Execute<TaskInfoOutputModel>(request);
            var result = response.Data;
            return result;
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