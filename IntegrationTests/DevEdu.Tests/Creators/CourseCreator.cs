using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Data;
using DevEdu.Tests.Constants;
using System.Collections.Generic;

namespace DevEdu.Tests.Creators
{
    public class CourseCreator : BaseCreator
    {
        public CourseInfoShortOutputModel AddCourse(string token)
        {
            _endPoint = CourseEndpoints.AddCourseEndpoint;
            var cource = CourseData.GetValidCourseInputModel();

            var request = _requestHelper.CreatePostRequest(_endPoint, cource, token);

            var response = _client.Execute<CourseInfoShortOutputModel>(request);
            var result = response.Data;
            return result;
        }

        public CourseInfoShortOutputModel UpdateCourse(string token, int courseId)
        {
            var cource = new CourseInputModel();
            return new CourseInfoShortOutputModel();
        }

        public void AddCourseMaterialReference(string token, int courseId, int materialId)
        {
        }

        public void AddTaskToCourse(string token, int courseId, int taskId)
        {
        }

        public CourseTopicOutputModel AddTopicToCourse(string token, int courseId, int topicId)
        {
            var model = new CourseTopicInputModel();
           return new CourseTopicOutputModel();
        }
        
        public List<CourseTopicOutputModel> AddTopicsToCourse(string token, int courseId, int topicId)
        {
            var models = new List<CourseTopicUpdateInputModel>();
           return new List<CourseTopicOutputModel>();
        }

        public List<CourseTopicOutputModel> UpdateCourseTopicsByCourseId(string token, int courseId)
        {
            var models = new List<CourseTopicUpdateInputModel>();
            return new List<CourseTopicOutputModel>();
        }
    }
}