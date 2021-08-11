using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Data;
using DevEdu.Tests.Constants;

namespace DevEdu.Tests.Creators
{
    public class CourseCreator : BaseCreator
    {
        public CourseInfoShortOutputModel CreateCorrectCourse(string token)
        {
            _endPoint = CoursePoints.AddCoursePoint;
            var postData = CourseData.GetValidCourseInputModel();

            var request = _requestHelper.Post(_endPoint, postData);
            request = _requestHelper.Autorize(request, token);

            var response = _client.Execute<CourseInfoShortOutputModel>(request);
            var result = response.Data;
            return result;
        }
    }
}