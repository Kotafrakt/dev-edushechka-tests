using DevEdu.Core.Enums;
using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Constants;
using DevEdu.Tests.Data;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;

namespace DevEdu.Tests.ControllersTests
{
    public class CourseControllerTest : BaseControllerTest
    {
        [TestCaseSource(typeof(UserRoleData), nameof(UserRoleData.GetRoleManager))]
        [TestCaseSource(typeof(UserRoleData), nameof(UserRoleData.GetRoleMethodist))]
        [TestCaseSource(typeof(UserRoleData), nameof(UserRoleData.GetRoleAdmin))]
        public void CreateCorrectCourse(List<Role> roles)
        {
            var user = _facade.RegisterUser(roles);
            var token = _facade.SignInUser(user.Email, user.Password);

            _endPoint = CoursePoints.AddCoursePoint;
            var postData = CourseData.GetInvalidCourseInputModel();

            var request = _requestHelper.Post(_endPoint, postData);
            request = _requestHelper.Autorize(request, token);

            var response = _client.Execute<CourseInfoShortOutputModel>(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = response.Data;
            postData.Should().BeEquivalentTo
            (
                result, options => options
                .Excluding(obj => obj.Id)
                .Excluding(obj => obj.Groups)
            );
        }
    }
}