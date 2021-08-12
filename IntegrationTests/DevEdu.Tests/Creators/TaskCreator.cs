using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Constants;
using DevEdu.Tests.Data;
using System.Collections.Generic;

namespace DevEdu.Tests.Creators
{
    public class TaskCreator : BaseCreator
    {
        public TaskInfoOutputModel CreateCorrectTaskByTeacherWithoutHomework(string token, List<int> tagIds = default)
        {
            _endPoint = TaskPoints.AddTaskByTeacherPoint;
            var postData = TaskData.GetValidTaskByTeacherWithoutHomework(tagIds);
            var request = _requestHelper.Post(_endPoint, postData);
            request = _requestHelper.Autorize(request, token);

            return _client.Execute<TaskInfoOutputModel>(request).Data;
        }

        public TaskInfoOutputModel CreateCorrectTaskByTeacherWithHomework(string token, int groupId, List<int> tagIds = default)
        {
            _endPoint = TaskPoints.AddTaskByTeacherPoint;
            var postData = TaskData.GetValidTaskByTeacherWithHomework(groupId, tagIds);
            var request = _requestHelper.Post(_endPoint, postData);
            request = _requestHelper.Autorize(request, token);

            return _client.Execute<TaskInfoOutputModel>(request).Data;
        }
    }
}