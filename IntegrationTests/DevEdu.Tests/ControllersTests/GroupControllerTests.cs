using DevEdu.Core.Enums;
using DevEdu.Tests.Data;
using DevEdu.Tests.Facades;
using NUnit.Framework;
using static DevEdu.Tests.Constants.GroupEndpoints;
using System.Collections.Generic;
using DevEdu.Core.Requests;
using DevEdu.Core.Models;
using FluentAssertions;
using System.Net;
using System.Linq;
using System;
using Newtonsoft.Json;

namespace DevEdu.Tests.ControllersTests
{
    public class GroupControllerTests : BaseControllerTest
    {
        private readonly AuthenticationFacade _authenticationFacade = new();
        private readonly GroupFacade _groupFacade = new();
        private readonly CourseFacade _courseFacade = new();

        [TestCase(Role.Manager)]
        public void AddGroup_GroupDto_GroupCreated(Role role)
        {
            //Given
            var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(role);
            _endPoint = AddGroupEndpoint;
            var token = _authenticationFacade.GetTokenByEmailAndPassword(userInfo.Email, userInfo.Password);
            var courdeId = _courseFacade.CreateCourse(token);
            var postData = GroupData.GetValidGroupInputModel(courdeId.Id);
            var request = _requestHelper.CreatePostRequest(_endPoint, postData, userInfo.Token);
            //When
            var response = _client.Execute<GroupOutputModel>(request);

            //Then
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var result = response.Data;
            postData.Should().BeEquivalentTo
            (
                result, options => options
                .Excluding(obj => obj.Id)
                .Excluding(obj => obj.Course)
                .Excluding(obj => obj.GroupStatus)
            );
        }

        [TestCase(Role.Methodist)]
        [TestCase(Role.Student)]
        [TestCase(Role.Tutor)]
        [TestCase(Role.Teacher)]
        public void AddGroup_AddGroupByUnauthorizedRole_Returned403StatusCode(Role role)
        {
            //Given
            var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var adminToken = _authenticationFacade.SignInByAdmin();
            _endPoint = AddGroupEndpoint;
            
            var courdeId = _courseFacade.CreateCourse(adminToken);
            var postData = GroupData.GetValidGroupInputModel(courdeId.Id);
            var request = _requestHelper.CreatePostRequest(_endPoint, postData, userInfo.Token);
            //When
            var actualResponce = _client.Execute(request);
            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
        [TestCase(Role.Manager)]
        public void AddGroup_AddGroupWithUncorrectDateFormat_Returned422StatusCode(Role role)
        {
            //Given
            var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(role);
            _endPoint = AddGroupEndpoint;
            var token = _authenticationFacade.GetTokenByEmailAndPassword(userInfo.Email, userInfo.Password);
            var courdeId = _courseFacade.CreateCourse(token);
            var postData = GroupData.GetValidGroupInputModel(courdeId.Id);
            postData.StartDate = "21,03,2021";
            var request = _requestHelper.CreatePostRequest(_endPoint, postData, userInfo.Token);
            //When
            var actualResponce = _client.Execute(request);
            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);

        }
        [TestCase(Role.Manager)]
        [TestCase(Role.Methodist)]
        [TestCase(Role.Student)]
        [TestCase(Role.Tutor)]
        [TestCase(Role.Teacher)]
        public void GetGroup_WithtAuthorizeAndWithValidId_GroupGotten(Role role)
        {
            //Given
            var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            var adminToken = _authenticationFacade.SignInByAdmin();
            _endPoint = AddGroupEndpoint;

            var courdeId = _courseFacade.CreateCourse(adminToken);
            var postData = GroupData.GetValidGroupInputModel(courdeId.Id);
            var postResponce = GroupData.CreateGroupInDbByAdminAndGetModel(_endPoint, postData, adminToken);
            var endPointForGet = string.Format(GetGroupEndpoint, postResponce.Id);

            var requestForGet = _requestHelper.CreateGetRequest(endPointForGet, userInfo.Token);
            //When
            var actualResponce = _client.Execute<GroupOutputModel>(requestForGet);
            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = actualResponce.Data;
            //postData.Should().BeEquivalentTo(result);
            result.Should().BeEquivalentTo
            (
                postData, options => options
                .Excluding(obj => obj.GroupStatusId)
                .Excluding(obj => obj.CourseId)
            );
        }
        [TestCase(Role.Manager)]
        [TestCase(Role.Methodist)]
        [TestCase(Role.Student)]
        [TestCase(Role.Tutor)]
        [TestCase(Role.Teacher)]
        public void GetGroup_WithtAuthorizeAndWithNotValidId_Returned404StatusCode(Role role)
        {
            //Given
            var invalidGroupId = 0;
            var userInfo = _authenticationFacade.RegisterNewUserAndSignIn(new List<Role> { role });
            _endPoint = string.Format(GetGroupEndpoint, invalidGroupId);
            var request = _requestHelper.CreateGetRequest(_endPoint, userInfo.Token);
            //When
            var actualResponce = _client.Execute(request);
            //Then
            actualResponce.StatusCode.Should().Be(HttpStatusCode.NotFound);

        }
    }
}
