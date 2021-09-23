using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Constants;
using DevEdu.Tests.Data;

namespace DevEdu.Tests.Creators
{
    public class StudentHomeworkCreator : BaseCreator
    {
        public StudentHomeworkWithHomeworkOutputModel AddStudentHomework(string token, int homeworkId)
        {
            _endPoint = StudentHomeworkEndpoints.AddStudentHomeworkEndpoint;
            var postData = HomeworkData.GetValidStudentHomeworkInputModel();
            var request = _requestHelper.CreatePostRequest(string.Format(_endPoint, homeworkId), postData, token);
            var response = _client.Execute<StudentHomeworkWithHomeworkOutputModel>(request);
            var result = response.Data;
            return result;
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