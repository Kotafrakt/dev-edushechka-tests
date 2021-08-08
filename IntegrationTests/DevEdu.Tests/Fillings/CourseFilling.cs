using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Data;
using FluentAssertions;
using Newtonsoft.Json;
using static DevEdu.Tests.ConstantPoints;

namespace DevEdu.Tests.Fillings
{
    public class CourseFilling : BaseFilling
    {
        public CourseInfoShortOutputModel CreateCorrectCourse(string token)
        {
            AuthenticateClient(token);

            _endPoint = AddCoursePoint;
            var postData = CourseData.GetCourseInputModel_Correct();

            var jsonData = JsonConvert.SerializeObject(postData);
            var request = _requestHelper.Post(_endPoint, _headers, jsonData);
            var response = _client.Execute<CourseInfoShortOutputModel>(request);
            var result = response.Data;

            postData.Should().BeEquivalentTo(result, options => options
                .Excluding(obj => obj.Id)
                .Excluding(obj => obj.Groups));

            CleanHeader();
            return result;
        }
    }
}