using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Data;
using DevEdu.Tests.Constants;

namespace DevEdu.Tests.Creators
{
    public class CourseCreator : BaseCreator
    {
        public CourseInfoShortOutputModel CreateValidCourse(string token)
        {
            _endPoint = CoursePoints.AddCoursePoint;
            var postData = CourseData.GetValidCourseInputModel();

            var request = _requestHelper.CreatePost(_endPoint, postData, token);

            var response = _client.Execute<CourseInfoShortOutputModel>(request);
            var result = response.Data;
            return result;
        }

    }
}