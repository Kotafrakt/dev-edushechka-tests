using DevEdu.Core.Enums;
using DevEdu.Core.Models;
using DevEdu.Core.Requests;
using DevEdu.Tests.Constants;
using DevEdu.Tests.Creators;
using DevEdu.Tests.Data;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using DevEdu.Tests.Facades;

namespace DevEdu.Tests.ControllersTests
{
    public class CourseControllerTest : BaseControllerTest
    {
        private AuthenticationCreator _creator = new();
        private readonly AuthenticationFacade _authenticationFacade = new();

        [TestCaseSource(typeof(UserRoleData), nameof(UserRoleData.GetRoleManager))]
        [TestCaseSource(typeof(UserRoleData), nameof(UserRoleData.GetRoleMethodist))]
        [TestCaseSource(typeof(UserRoleData), nameof(UserRoleData.GetRoleAdmin))]
        public void CreateCorrectCourse(Role role)
        {
            var userInfo = _authenticationFacade.SignInByAdminAndRegistrationNewUserByRole(role);

            _endPoint = CourseEndpoints.AddCourseEndpoint;
            var postData = CourseData.GetValidCourseInputModel();

            var request = _requestHelper.CreatePostRequest(_endPoint, postData, userInfo.Token);

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